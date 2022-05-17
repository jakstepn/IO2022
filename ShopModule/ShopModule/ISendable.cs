using ShopModule_ApiClasses.Messages;

namespace ShopModule
{
    public interface ISendable
    {
        public void Accept(IVisitor visitor);
    }
}
