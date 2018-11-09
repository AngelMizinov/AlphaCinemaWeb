using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaCinemaData.Models.Associative;

namespace AlphaCinemaServices.Contracts
{
    public interface IProjectionService
    {
        Task<IEnumerable<Projection>> GetByTownId(int townId, string userId, DayOfWeek? day = null);

        Task<IEnumerable<Projection>> GetTopProjections(int count);

        Task<WatchedMovie> AddReservation(string userId, int projectionId);

        Task<WatchedMovie> DeclineReservation(string userId, int projectionId);
    }
}