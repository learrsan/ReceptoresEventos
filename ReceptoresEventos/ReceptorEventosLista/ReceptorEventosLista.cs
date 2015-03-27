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
            base.ItemAdding(properties);
        }

        public override void itemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
        }
        
        public override void itemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);
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