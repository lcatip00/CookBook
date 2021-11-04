using CookBookDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookDAL.Data
{
    public interface IStepRepository
    {
        Task<Step> CreateStep(Step step);
        Task DeleteStep(Step step);
        Task<Step> GetStep(int stepId);
        Task<Step> UpdateStep(Step step);
    }
}
