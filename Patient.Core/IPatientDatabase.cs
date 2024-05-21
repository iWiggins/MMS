using Patient.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Core;
public interface IPatientDatabase
{
	Task<PatientInfo> CreatePatientAsync(PatientInfo patient);
	Task<bool> DeletePatientAsync(PatientInfo patient);
	Task<PatientInfo> GetPatientsAsync();
	Task<IEnumerable<PrescriptionInfo>> GetPatientPrescriptionsAsync(PatientInfo patient);
	Task<PrescriptionInfo> AddPrescriptionAsync(PatientInfo patient, PrescriptionInfo prescription);
	Task<bool> DeletePrescriptionAsync(PatientInfo patient, PrescriptionInfo prescription);
	Task<IEnumerable<ProviderInfo>> GetPatientProvidersAsync(PatientInfo patient);
	Task<ProviderInfo> GetPrescriptionProviderAsync(PrescriptionInfo prescription);
}
