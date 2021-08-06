using TimeProject.Domain.Commands.TimeSheets;

namespace TimeProject.Domain.Validations.TimeSheets
{
    public class InsertTimeSheetValidation : TimeSheetValidation<InsertTimeSheetCommand>
    {
        public InsertTimeSheetValidation()
        {
            DescriptionValidation();
            ProjectIdValidation();
            ActivityIdValidation();
            StartDateValidation();
            UserIdValidation();
        }
    }
}
