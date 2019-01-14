namespace CSharp.Saude.FitbitTask
{
    using System;

    public interface IId
    {
        Guid Id { get; }
    }
    public struct OnlyId : IId
    {
        public Guid Id;
        Guid IId.Id => Id;
        public OnlyId(string id)
        {
            this.Id = new Guid(id);
        }
    }
}
