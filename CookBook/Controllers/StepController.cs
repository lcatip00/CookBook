using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBookDAL.Data;
using CookBookDAL.Models;
using Microsoft.AspNetCore.Http;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IStepRepository stepRepository;

        public StepController(IStepRepository stepRepository)
        {
            this.stepRepository = stepRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateStep([FromBody]Step step)
        {
            try
            {
                if (step == null)
                    return BadRequest();

                var createdStep = await stepRepository.CreateStep(step);
                return CreatedAtAction(nameof(GetStep), new { id = createdStep }, createdStep);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new step");
            };

        }
        [HttpDelete("{stepId:int}")]
        public async Task<ActionResult> DeleteStep(int stepId)
        {
            try
            {
                var step = await stepRepository.GetStep(stepId);
                if(step == null)
                {
                    return NotFound($"Step with id={stepId} not found ");
                }

                await stepRepository.DeleteStep(step);
                return Ok($"Step with id={stepId} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting step");
            }
        }

        [HttpGet("{stepId:int}")]
        public async Task<ActionResult<Step>> GetStep(int stepId)
        {
            try
            {
                var step = await stepRepository.GetStep(stepId);
                if(step == null)
                {
                    return NotFound();
                }

                return step;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        
        [HttpPut("{stepId:int}")]
        public async Task<ActionResult<Step>> UpdateStep([FromRoute]int stepId, [FromBody] Step step)
        {
            try
            {
                if (stepId != step.StepId)
                {
                    return BadRequest("Step Id mismatch!");
                }

                var updateStep = await stepRepository.UpdateStep(step);

                if(updateStep == null)
                {
                    return NotFound($"Step with id={step.StepId} not found");
                }

                return await stepRepository.UpdateStep(step);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating step ");
            }
        }
    }
}
