using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractice
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
    }
}
