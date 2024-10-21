using ControledeSalaFEMASS.Domain.Extensions;
using FluentValidation;

namespace ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Criar;
public class CriarIndisponibilidadeCommandValidator : AbstractValidator<CriarIndisponibilidadeCommand>
{
    public CriarIndisponibilidadeCommandValidator()
    {
        RuleFor(p => p.DiaSemana)
                    .Must(dia => dia >= DayOfWeek.Monday && dia <= DayOfWeek.Friday)
                    .WithMessage("O dia da semana deve ser entre segunda-feira e sexta.");

        RuleFor(p => p.Tempo).IsInEnum().WithMessage("O código horário não é valido. O valor deve estar entre 1 e 3");
    }
}