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

        RuleFor(p => p.CodigoHorario).InclusiveBetween(1, 3).WithMessage("O código horário deve estar entre 1 e 3");
    }
}