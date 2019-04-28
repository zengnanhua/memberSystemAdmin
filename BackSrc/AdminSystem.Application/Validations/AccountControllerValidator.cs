using AdminSystem.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Validations
{
    public class GetTokenCommandValidator:AbstractValidator<GetTokenCommand>
    {
        public GetTokenCommandValidator()
        {
            RuleFor(c => c.IdentityName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(c => c.Password).NotEmpty().WithMessage("密码不能为空");
            
        }
    }
}
