using global::TechTalk.SpecFlow;

public class Active_Active_SpecFlowPlusRunnerAssemblyHooks
{
    [global::TechTalk.SpecRun.AssemblyInitialize]
    public static void AssemblyInitialize()
    {
        TestRunnerManager.OnTestRunStart();
    }

    [global::TechTalk.SpecRun.AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        TestRunnerManager.OnTestRunEnd();
    }
}
