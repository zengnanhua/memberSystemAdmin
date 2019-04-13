using AdminSystem.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Validations
{
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("用户名不能为空");
        }
    }
}
