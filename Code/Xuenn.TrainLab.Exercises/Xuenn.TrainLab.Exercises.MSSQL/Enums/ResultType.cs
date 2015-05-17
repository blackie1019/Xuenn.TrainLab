namespace Xuenn.TrainLab.Exercises.MSSQL.Enums
{
    public enum ResultType
    {
        Success=100000,
        FailedScriptEmpty = 200010,
        FailedScriptWrongColumnNumber = 200020,
        FailedScriptInconsistent = 200030,
        FailedResetCompletely = 300010,
        FailedScriptWrongAnswer= 900000,
        Exception=999999
    }
}
