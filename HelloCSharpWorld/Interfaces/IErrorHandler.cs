using HelloCSharpWorld.Core;

namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: Manuel olarak hata yönetiminin yapıldığı ve kullanıcının 
    ///         kırmızı hata mesajıyla bilgilendirildiği arayüz.
    ///     EN: An interface in which error handling is performed manually
    ///         and the user is informed via red error messages.
    /// </summary>
    public interface IErrorHandler
    {
        void HandleError(Error error);
    }
}
