using TimeProject.Domain.Commands.TimeSheets;

namespace TimeProject.Domain.Validations.TimeSheets
{
    public class UpdateTimeSheetValidation : TimeSheetValidation<UpdateTimeSheetCommand>
    {
        public UpdateTimeSheetValidation()
        {
            IdValidation();
            DescriptionValidation();
            ProjectIdValidation();
            ActivityIdValidation();
            StartDateValidation();
            UserIdValidation();
        }
    }
}
