﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SecureBank.Ctf.Models;
using System;
using System.Linq;

namespace SecureBank.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HiddenPageAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            CtfOptions ctfOptions =
                context.HttpContext.RequestServices
                .GetRequiredService<IOptions<CtfOptions>>().Value;

            if (!ctfOptions.IsCtfEnabled)
            {
                context.Result = new OkObjectResult("Admin Registration");
            }
            else if (ctfOptions.CtfChallengeOptions.HiddenPageRegisterAdmin)
            {
                CtfChallangeModel hiddenPageChallange = ctfOptions.CtfChallanges
                    .Where(x => x.Type == CtfChallengeTypes.HiddenPage)
                    .Single();

                context.Result = new OkObjectResult(hiddenPageChallange.Flag);
            }
            else
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
