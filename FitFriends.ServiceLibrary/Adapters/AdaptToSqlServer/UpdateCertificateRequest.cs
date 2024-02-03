using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Adapters.AdaptToSqlServer
{
    public class UpdateCertificateRequest
    {
        public UpdateCertificateRequest(CertificateEntity entity)
        {
            CertificateId = entity.CertificateId;
            UserId = entity.UserId;
            ImageId = entity.ImageId;
            CertificateName = entity.CertificateName;
        }

        public Guid CertificateId { get; set; }

        public Guid UserId { get; set; }

        public Guid? ImageId { get; set; }

        public string CertificateName { get; set; }
    }
}
