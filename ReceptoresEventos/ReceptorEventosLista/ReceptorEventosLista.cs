using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace ReceptoresEventos.ReceptorEventosLista
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ReceptorEventosLista : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            var precio = Convert.ToDouble(properties.AfterProperties["j9l5"]);
            ActualizacionPropiedades(properties.Web,precio);
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            var precioAnterior = (double) properties.ListItem["j9l5"];
            var precio = Convert.ToDouble(properties.AfterProperties["j9l5"]);
            var total = precio - precioAnterior;
            ActualizacionPropiedades(properties.Web, total); 
        }
        
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            var precio = Convert.ToDouble(properties.ListItem["j9l5"]);
            ActualizacionPropiedades(properties.Web, -precio);
        }

        private void ActualizacionPropiedades(SPWeb web, double precio)
        {
            string clave = "TotalImporte";

            double actual = 0;

            if (web.AllProperties[clave] != null)
            {
                actual = Convert.ToDouble(web.AllProperties[clave]);
            }
            else
            {
                web.AllProperties.Add(clave,"");
            }
            
            actual += precio;

            web.AllProperties[clave] = actual.ToString();
            
        }
    }
}