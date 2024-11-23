using ControledeSalaFEMASS.Domain.Services.QuestPDF;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Sala.GetSalasDocumentByDay
{
	public class GetSalasDocumentByDayQueryHandler : IRequestHandler<GetSalasDocumentByDayQuery, byte[]>
	{
		private readonly IDocumentGenerator _documentGenerator;

		public GetSalasDocumentByDayQueryHandler(
			IDocumentGenerator documentGenerator)
		{
			_documentGenerator = documentGenerator;
		}

		public async Task<byte[]> Handle(GetSalasDocumentByDayQuery request, CancellationToken cancellationToken)
		{
			var document = await _documentGenerator.GetSalasByDay(request.DayOfWeek);

			return document;
		}
	}
}
