using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaServices
{
    public class ProjectionService : IProjectionService
    {
        private readonly AlphaCinemaContext context;
        private readonly int hour = DateTime.Now.Hour;
        private readonly int minute = DateTime.Now.Minute;
        private readonly int dateDay = DateTime.Now.Day;
        private readonly int dateMonth = DateTime.Now.Month;
        private readonly int dateYear = DateTime.Now.Year;
        private readonly int currentDay = (int)DateTime.Now.DayOfWeek;

        public ProjectionService(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Projection>> GetByTownId(int townId, string userId, DayOfWeek? day = null)//Трябва да добавя и UserId
        {
            day = day ?? DateTime.Now.DayOfWeek;

            try
            {
                //Взимаме запазените места за деня за всяка прожекция, които не са били изтрити
                var bookings = await this.context.WatchedMovies
                    .Where(booking => booking.IsDeleted == false)
                    .Where(booking => booking.Date.Year == dateYear && booking.Date.Month == dateMonth && booking.Date.Day == dateDay)
                    .ToListAsync();

                //Създаваме речник, в който ключа ни е Id-то на прожекцията, а стойността броя хора които са я резервирали
                var projectionSeats = GetSeatsForEachProjection(bookings);

                //Тук пазим резервациите на нашия User и ако сме подали такъв ги взимаме
                var userBookings = new HashSet<int>();
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    userBookings = GetBookingsOfUser(bookings, userId);
                }

                var projections = await this.context.Projections
                    .Where(p => p.CityId == townId)
                    .Include(p => p.OpenHour)
                    .Include(p => p.Movie)
                        .ThenInclude(m => m.MovieGenres)
                            .ThenInclude(mg => mg.Genre)
                    .Where(p => p.Day == (int)day)//Тук филтрираме за текущия ден от седмицата
                    .ToListAsync();

                //Ако сме в днешния филтрираме по часове
                if (currentDay == (int)day)
                {//Тук казваме че или отворения час е след настоящия или е равен на него, но минутите са след настоящите
                    projections = projections.Where(p => (p.OpenHour.Hours > hour) || (p.OpenHour.Hours == hour && p.OpenHour.Minutes >= minute)).ToList();
                }

                projections.ForEach(p => { p.Seats -= projectionSeats.TryGetValue(p.Id, out int taken) ? taken : 0; p.IsBooked = userBookings.Contains(p.Id); });
                //Вадим от всяка прожекция местата и за деня и проверяваме дали в booking-а на нашия user има съответната прожекция
                return projections;
            }
            catch (Exception ex)
            {
                throw new EntityDoesntExistException("Projection with that UserId and TownId cannot be found");
            }

        }

        public async Task<IEnumerable<Projection>> GetTopProjections(int count)
        {
            return await this.context.Projections
                .Include(p => p.OpenHour)
                .Include(p => p.Movie)
                    .ThenInclude(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                .GroupBy(p => p.Movie)
                .Select(group => group.First())
                .Take(count)
                .ToListAsync();
        }

        public async Task<WatchedMovie> AddReservation(string userId, int projectionId)
        {
            try
            {
                var reservation = CheckIfReservationExist(userId, projectionId);

                if (reservation == null)
                {
                    reservation = new WatchedMovie()
                    {
                        UserId = userId,
                        ProjectionId = projectionId,
                        Date = DateTime.Now
                    };
                    this.context.Add(reservation);
                }
                else
                {
                    reservation.Date = DateTime.Now;
                    reservation.IsDeleted = false;
                    reservation.DeletedOn = null;
                    this.context.Update(reservation);
                }

                await this.context.SaveChangesAsync();
                return reservation;
            }
            catch (Exception ex)
            {
                throw new EntityDoesntExistException("Reservation with that UserId and ProjectionId cannot be booked");
            }
        }


        public async Task<WatchedMovie> DeclineReservation(string userId, int projectionId)
        {
            try
            {
                var currentBooking = this.context.WatchedMovies
                    .Where(booking => booking.Date.Year == dateYear && booking.Date.Month == dateMonth && booking.Date.Day == dateDay)
                    .Where(booking => booking.UserId == userId && booking.ProjectionId == projectionId)
                    .FirstOrDefault();

                this.context.WatchedMovies.Remove(currentBooking);
                await this.context.SaveChangesAsync();

                return currentBooking;
            }
            catch (Exception)
            {
                throw new EntityDoesntExistException("Reservation with that UserId and ProjectionId cannot be declined");
            }
        }

        private HashSet<int> GetBookingsOfUser(List<WatchedMovie> bookings, string userId)
        {
            return bookings
                .Where(booking => booking.UserId == userId)
                .Select(booking => booking.ProjectionId)
                .ToHashSet();
        }

        private Dictionary<int, int> GetSeatsForEachProjection(List<WatchedMovie> bookings)
        {
            return bookings.GroupBy(booking => booking.ProjectionId)
                .Select(group => new KeyValuePair<int, int>
                (
                    group.Key,
                    group.Count()
                )).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private WatchedMovie CheckIfReservationExist(string userId, int projectionId)
        {
            return this.context.WatchedMovies
                .Where(booking => booking.Date.Year == dateYear && booking.Date.Month == dateMonth && booking.Date.Day == dateDay)
                .Where(booking => booking.UserId == userId && booking.ProjectionId == projectionId)
                .FirstOrDefault();
        }
    }
}
