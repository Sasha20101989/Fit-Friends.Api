﻿using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Adapters.AdaptToSqlServer
{
    public class CreateCertificateRequest
    {
        public CreateCertificateRequest(CertificateEntity entity)
        {
            CertificateId = entity.CertificateId;
            UserId = entity.UserId;
            CertificateName = entity.CertificateName;
        }

        public Guid CertificateId { get; set; }

        public Guid UserId { get; set; }

        public string CertificateName { get; set; }
    }
}
