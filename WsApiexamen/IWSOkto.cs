using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WsApiexamen
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWSOkto
    {

        [OperationContract]
        List<string> AgregarExamen(int id, string Nombre, string Descripcion);

        [OperationContract]
        List<tblExamen> GetTblExamens();

        [OperationContract]
        List<tblExamen> ConsultarExamen(int id, string Nombre, string Descripcion);

        [OperationContract]
        List<string> EliminarExamen(int id);

        [OperationContract]
        List<string> ActualizarExamen(int id, string Nombre, string Descripcion);

        [OperationContract]
        List<tblExamen> ConsultarExamenbyId(int id);

    }
}
