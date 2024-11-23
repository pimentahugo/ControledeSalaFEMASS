using ControledeSalaFEMASS.Domain.Enums;
using ControledeSalaFEMASS.Domain.Repositories;
using ControledeSalaFEMASS.Domain.Services.QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ControledeSalaFEMASS.Infrastructure.Services.QuestPDF
{
	public class DocumentGenerator : IDocumentGenerator
	{
		private readonly ISalaRepository _salasRepository;
		private readonly ITurmaRepository _turmaRepository;

		private static readonly Dictionary<DayOfWeek, List<TempoSala>> _horariosPorDia = new()
		{
			{ DayOfWeek.Monday, new List<TempoSala>() { TempoSala.Tempo1, TempoSala.Tempo3 }},
			{ DayOfWeek.Tuesday, new List<TempoSala>() { TempoSala.Tempo1, TempoSala.Tempo2, TempoSala.Tempo3 }},
			{ DayOfWeek.Wednesday, new List<TempoSala>() { TempoSala.Tempo1, TempoSala.Tempo3 }},
			{ DayOfWeek.Thursday, new List<TempoSala>() { TempoSala.Tempo1, TempoSala.Tempo2, TempoSala.Tempo3 }},
			{ DayOfWeek.Friday, new List<TempoSala>() { TempoSala.Tempo1, TempoSala.Tempo2 }},
		};

		private static string GetHorarioEquivalente(TempoSala tempo, DayOfWeek dia) => (tempo, dia) switch
		{
			(TempoSala.Tempo1, _) => "18:00",
			(TempoSala.Tempo2, DayOfWeek.Friday) => "19:50",
			(TempoSala.Tempo2, _) => "19:40",
			(TempoSala.Tempo3, _) => "20:40",
			_ => string.Empty
		};

		private static readonly Dictionary<DayOfWeek, string> _diasEquivalente = new()
		{
			{ DayOfWeek.Monday, "Segunda-feira" },
			{ DayOfWeek.Tuesday, "Terça-feira" },
			{ DayOfWeek.Wednesday, "Quarta-feira" },
			{ DayOfWeek.Thursday, "Quinta-feira" },
			{ DayOfWeek.Friday, "Sexta-feira" }
		};

		public DocumentGenerator(
			ISalaRepository salasRepository,
			ITurmaRepository turmaRepository)
		{
			_salasRepository = salasRepository;
			_turmaRepository = turmaRepository;
		}

		public async Task<byte[]> GetSalasByDay(DayOfWeek dayOfWeek)
		{
			var salas = await _salasRepository.GetAll();
			var turmasAlocadas = await _turmaRepository.GetAlocacoesByDay(dayOfWeek);
			var horarios = _horariosPorDia[dayOfWeek];

			var document = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(1, Unit.Centimetre);
					page.DefaultTextStyle(x => x.FontSize(10));

					// Header
					page.Header().ShowOnce().Column(column =>
					{
						column.Item().PaddingBottom(10).Row(row =>
						{
							row.RelativeItem().Column(header =>
							{
								header.Item().Text("Faculdade Professor Miguel Ângelo - FEMASS")
									.SemiBold()
									.FontSize(16)
									.AlignCenter();

								header.Item().Text($"Quadro de Horários - {_diasEquivalente[dayOfWeek]}")
									.FontSize(14)
									.AlignCenter();
							});
						});
					});

					// Content
					page.Content().Column(stack =>
					{
						// Iterando primeiro por horário
						foreach (var horario in horarios)
						{
							// Iterando por bloco
							foreach (var bloco in salas.GroupBy(t => t.Bloco).OrderBy(p => p.Key))
							{
								// Block and time header
								stack.Item().BorderBottom(1).BorderColor("#000000").PaddingBottom(5)
									.Text($"BLOCO {bloco.Key}")
									.AlignCenter()
									.SemiBold()
									.FontSize(14);

								// Table
								stack.Item().Table(table =>
								{
									// Define columns
									table.ColumnsDefinition(columns =>
									{
										columns.ConstantColumn(50);  // Turma
										columns.RelativeColumn(3);   // Disciplina
										columns.RelativeColumn(2);   // Professor
										columns.ConstantColumn(60);  // Horário
										columns.ConstantColumn(50);  // Sala
									});

									// Header row
									table.Header(header =>
									{
										header.Cell().Background("#E0E0E0").Padding(5).AlignCenter().Text("TURMA").SemiBold();
										header.Cell().Background("#E0E0E0").Padding(5).AlignLeft().Text("DISCIPLINA").SemiBold();
										header.Cell().Background("#E0E0E0").Padding(5).AlignCenter().Text("PROFESSOR").SemiBold();
										header.Cell().Background("#E0E0E0").Padding(5).AlignLeft().Text("HORÁRIO").SemiBold();
										header.Cell().Background("#E0E0E0").Padding(5).AlignLeft().Text("SALA").SemiBold();
									});

									// Mostrar todas as salas do bloco
									foreach (var sala in bloco.OrderBy(s => s.Numero))
									{
										var turmaAlocada = turmasAlocadas.FirstOrDefault(t =>
											t.Tempo == horario &&
											t.Sala.Numero == sala.Numero &&
											t.Sala.Bloco == sala.Bloco);

										// Turma
										table.Cell().BorderBottom(1).BorderColor("#E0E0E0").Padding(5)
											.AlignCenter()
											.Text(turmaAlocada != null ? turmaAlocada.Turma.Id : "");

										// Disciplina
										table.Cell().BorderBottom(1).BorderColor("#E0E0E0").Padding(5)
											.AlignLeft()
											.Text(turmaAlocada != null ? turmaAlocada.Turma.Disciplina.Nome : "");

										// Professor
										table.Cell().BorderBottom(1).BorderColor("#E0E0E0").Padding(5)
											.AlignLeft()
											.Text(turmaAlocada != null ? turmaAlocada.Turma.Professor : "");

										table.Cell().BorderBottom(1).BorderColor("#E0E0E0").Padding(5)
											.AlignLeft()
											.Text(GetHorarioEquivalente(horario, dayOfWeek));

										table.Cell().BorderBottom(1).BorderColor("#E0E0E0").Padding(5)
											.AlignCenter()
											.Text(sala.Numero.ToString());
									}
								});

								stack.Item().PaddingBottom(20);
							}
						}
					});

					page.Footer()
						.AlignCenter()
						.Text(x =>
						{
							x.Span("Página ");
							x.CurrentPageNumber();
							x.Span(" de ");
							x.TotalPages();
						});
				});
			});

			return document.GeneratePdf();
		}
	}
}