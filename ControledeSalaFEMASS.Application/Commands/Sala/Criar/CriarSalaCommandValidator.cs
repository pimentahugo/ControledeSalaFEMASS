using ControledeSalaFEMASS.Domain.Extensions;
using FluentValidation;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Criar;
public class CriarSalaCommandValidator : AbstractValidator<CriarSalaCommand>
{
    public CriarSalaCommandValidator()
    {
        RuleFor(p => p.Bloco).NotEmpty().WithMessage("O bloco deve ser informado.");
        RuleFor(p => p.Numero).NotEmpty().WithMessage("O número da sala deve ser informado");

        When(p => p.Bloco.NotEmpty(), () =>
        {
            RuleFor(p => p.Bloco).Matches("^[A-Z]$").WithMessage("Bloco deve ser uma letra de A a Z.");
        });

        RuleFor(p => p.CapacidadeMaxima).GreaterThan(0).WithMessage("O valor de capacidade máxima deve ser maior que 0");
    }
}