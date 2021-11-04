using CookBookDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookDAL.Data
{
    public class StepRepository : IStepRepository
    {
        private readonly CookBookContext cookBookContext;

        public StepRepository(CookBookContext cookBookContext)
        {
            this.cookBookContext = cookBookContext;
        }
        public async Task<Step> CreateStep(Step step)
        {
            var stepToSave = await cookBookContext.Steps.AddAsync(step);
            await cookBookContext.SaveChangesAsync();
            return stepToSave.Entity;
        }

        public async Task DeleteStep(Step step)
        {
                cookBookContext.Steps.Remove(step);
                await cookBookContext.SaveChangesAsync();
        }

        public async Task<Step> GetStep(int stepId)
        {
            var step = await cookBookContext.Steps.FirstOrDefaultAsync(s => s.StepId == stepId);

            return step;
        }

        public async Task<Step> UpdateStep(Step step)
        {
            var updateStep = await cookBookContext.Steps.FirstOrDefaultAsync(s => s.StepId == step.StepId);

            if(updateStep != null)
            {
                updateStep.StepNumber = step.StepNumber;
                updateStep.StepDescription = step.StepDescription;

                await cookBookContext.SaveChangesAsync();

                return updateStep;
            }

            return null;
        }
    }
}
