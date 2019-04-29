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
            RuleFor(c => c.Name).NotEmpty().WithMessage("用户名字不能为空");
            RuleFor(c => c.Sex).NotEmpty().WithMessage("用户性别不能为空");
            RuleFor(c => c.UserName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(c => c.Phone).NotEmpty().WithMessage("手机号码不能为空");
            RuleFor(c => c.Pwd).NotEmpty().WithMessage("密码不能为空");
        }
    }
}
