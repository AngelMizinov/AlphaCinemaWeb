using System.Collections.Generic;
using AlphaCinemaData.Models.Associative;

namespace AlphaCinemaServices.Contracts
{
    public interface IProjectionService
    {
        IEnumerable<Projection> GetByTownId(int townId);

        IEnumerable<Projection> GetTopProjections(int count);
    }
}