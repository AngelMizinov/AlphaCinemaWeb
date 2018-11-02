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

        public IEnumerable<Projection> GetByTownId(int townId)
        {
            return this.context.Projections
                .Where(p => p.CityId == townId)
                .Include(p => p.OpenHour)
                .Where(p => p.OpenHour.Hours >= DateTime.Now.Hour && p.OpenHour.Minutes >= DateTime.Now.Minute)
                .Include(p => p.Movie)
                    .ThenInclude(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                .ToList();
        } 
    }
}
