using EvaluationSpaceAPI.EFContext;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;

namespace EvaluationSpaceAPI.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(EvaluationSpaceDbContext context) : base(context) { }
    }
}
