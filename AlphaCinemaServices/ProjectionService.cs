using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class ProjectionService : IProjectionService
    {
        private readonly AlphaCinemaContext context;

        public ProjectionService(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Projection> GetByTownId(int townId, string userId, DayOfWeek? day = null)//Трябва да добавя и UserId
        {
            var hour = DateTime.Now.Hour;
            var minute = DateTime.Now.Minute;
            var dateDay = DateTime.Now.Day;
            var dateMonth = DateTime.Now.Month;
            var dateYear = DateTime.Now.Year;
            day = day ?? DateTime.Now.DayOfWeek;

            //Взимаме запазените места за деня за всяка прожекция, които не са били изтрити по някаква причина
            var bookings = this.context.WatchedMovies
                .Where(booking => booking.IsDeleted == false)
                .Where(booking => booking.Date.Year == dateYear && booking.Date.Month == dateMonth && booking.Date.Day == dateDay);

            //Създаваме речник, в който ключа ни е Id-то на прожекцията, стойността броя хора които са я резервирали
            var projectionSeats = bookings.GroupBy(booking => booking.ProjectionId)
                .Select(group => new KeyValuePair<int, int>
                (
                    group.Key,
                    group.Count()
                )).ToDictionary(pair => pair.Key, pair => pair.Value);

            //Тук пазим резервациите на нашия User
            var userBookings = new HashSet<int>();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                userBookings = bookings
                    .Where(booking => booking.UserId == userId)
                    .Select(booking => booking.ProjectionId)
                    .ToHashSet();
            }


            //TODO: Fix the to show the projections for the next days for all hours
            var projections = this.context.Projections
                .Where(p => p.CityId == townId)
                .Include(p => p.OpenHour)
                //.Where(p => (p.OpenHour.Hours > hour) || (p.OpenHour.Hours == hour && p.OpenHour.Minutes >= minute))
                //Тук казваме че или отворения час е след настоящия или е равен на него, но минутите са след настоящите
                .Include(p => p.Movie)
                    .ThenInclude(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                //.Where(p => p.Day == (int)day)//Тук ги взимаме за текущия ден от седмицата
                .ToList();

            projections.ForEach(p => { p.Seats -= projectionSeats.TryGetValue(p.Id, out int taken) ? taken : 0; p.IsBooked = userBookings.Contains(p.Id); });
            //Вадим от всяка прожекция местата и за деня и проверяваме дали в booking-а на нашия user има съответната прожекция
            return projections;
        }

        public IEnumerable<Projection> GetTopProjections(int count)
        {
            return this.context.Projections
                .Include(p => p.OpenHour)
                .Include(p => p.Movie)
                    .ThenInclude(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                .GroupBy(p => p.Movie)
                .Select(group => group.First())
                .Take(count)
                .ToList();
        }

        public WatchedMovie AddReservation(string userId, int projectionId)
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
                this.context.Update(reservation);
            }

            this.context.SaveChanges();
            return reservation;
        }

        private WatchedMovie CheckIfReservationExist(string userId, int projectionId)
        {
            var dateDay = DateTime.Now.Day;
            var dateMonth = DateTime.Now.Month;
            var dateYear = DateTime.Now.Year;

            return this.context.WatchedMovies
                .Where(booking => booking.Date.Year == dateYear && booking.Date.Month == dateMonth && booking.Date.Day == dateDay)
                .Where(booking => booking.UserId == userId && booking.ProjectionId == projectionId)
                .FirstOrDefault();
        }

        public WatchedMovie DeclineReservation(string userId, int projectionId)
        {
            var dateDay = DateTime.Now.Day;
            var dateMonth = DateTime.Now.Month;
            var dateYear = DateTime.Now.Year;

            var currentBooking = this.context.WatchedMovies
                .Where(booking => booking.Date.Year == dateYear && booking.Date.Month == dateMonth && booking.Date.Day == dateDay)
                .Where(booking => booking.UserId == userId && booking.ProjectionId == projectionId)
                .FirstOrDefault();

            this.context.WatchedMovies.Remove(currentBooking);
            this.context.SaveChanges();

            return currentBooking;
        }
    }
}
