using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.ProjectBuilding
{
    public interface IProjectBuilder
    {
        public Task CreateProject(ProjectBuildingContext context);
    }
}
