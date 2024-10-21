using ControledeSalaFEMASS.Domain.Extensions;
using FluentValidation;

namespace ControledeSalaFEMASS.Application.Commands.Sala;
public class SalaCommandBaseValidator<T> : AbstractValidator<T> where T : SalaCommandBase
{
    public SalaCommandBaseValidator()
    {
        RuleFor(p => p.CapacidadeMaxima).GreaterThan(0).WithMessage("O valor de capacidade máxima deve ser maior que 0");
    }
}