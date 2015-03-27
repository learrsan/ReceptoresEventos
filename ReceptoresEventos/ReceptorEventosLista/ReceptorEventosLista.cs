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
            var precio = Convert.ToDouble(properties.AfterProperties["Precio"]);
            ActualizacionPropiedades(properties.Web,precio);
        }

        public override void itemUpdating(SPItemEventProperties properties)
        {
            var precioAnterior = (double) properties.ListItem["Precio"];
            var precio = Convert.ToDouble(properties.AfterProperties["Precio"]);
            var total = precio - precioAnterior;
            ActualizacionPropiedades(properties.Web, total); 
        }
        
        public override void itemDeleting(SPItemEventProperties properties)
        {
            var precio = Convert.ToDouble(properties.AfterProperties["Precio"]);
            ActualizacionPropiedades(properties.Web, -precio);
        }

        private void ActualizacionPropiedades(SPWeb web, double precio)
        {
            string clave = "TotalImporte";

            double actual = 0;

            if (web.Properties[clave] != null)
            {
                actual = Convert.ToDouble(web.Properties[clave]);
            }
            else
            {
                web.Properties.Add(clave,"");
            }
            
            actual += precio;

            web.Properties[clave] = actual.ToString();
            web.Properties.Update();
        }
    }
}