namespace ControledeSalaFEMASS.Domain.Services.QuestPDF;
public interface IDocumentGenerator
{
	Task<byte[]> GetSalasByDay(DayOfWeek dayOfWeek);
}