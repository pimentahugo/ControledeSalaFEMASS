using FluentValidation;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.AlocarSala;
public class AlocarSalaCommandValidator : AbstractValidator<AlocarTurmaSalaCommand>
{
	public AlocarSalaCommandValidator()
	{
		RuleFor(p => p.DiaSemana).IsInEnum().WithMessage("Informe um dia de semana válido");
		RuleFor(p => p.TempoSala).IsInEnum().WithMessage("Informe um horário válido");
	}
}