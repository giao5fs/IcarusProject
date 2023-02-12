using FluentValidation;
using Icarus.Application.Members.RegisterMember;
using Icarus.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Application.Members.Commands.RegisterMember
{
    internal class RegisterMemberCommandValidator : AbstractValidator<RegisterMemberCommand>
    {
        public RegisterMemberCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(LastName.MaxLength);
        }
    }
}
