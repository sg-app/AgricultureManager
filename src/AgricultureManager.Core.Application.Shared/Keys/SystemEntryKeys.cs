namespace AgricultureManager.Core.Application.Shared.Keys
{
    public static class SystemEntryKeys
    {
        public static readonly Guid FertilizerDetailN = Guid.Parse("04433ca8-714f-4007-bd93-672b2d10ff36");
        public static readonly Guid FertilizerDetailP = Guid.Parse("0d69cc79-e5b4-4c84-afed-0f9397a611cb");
        public static readonly Guid FertilizerDetailK = Guid.Parse("1b5bb848-475d-4d77-bf53-d3d6ff09db46");
        public static readonly Guid FertilizerDetailS = Guid.Parse("8cfee622-ef2f-44ec-b6ca-92db0e8ee8fe");

        public static IEnumerable<Guid> FertilizerDetailKeys =>
        [
            FertilizerDetailN,
            FertilizerDetailP,
            FertilizerDetailK,
            FertilizerDetailS
        ];

    }
}
