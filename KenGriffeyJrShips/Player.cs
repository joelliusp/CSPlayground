//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KenGriffeyJrShips
{
    using System;
    using System.Collections.Generic;
    
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            this.Fans = new HashSet<Fan>();
        }
    
        public int PlayerPK { get; set; }
        public string PlayerName { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<int> TeamFK { get; set; }
        public Nullable<int> PositionFK { get; set; }
    
        public virtual LKPosition LKPosition { get; set; }
        public virtual Team Team { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fan> Fans { get; set; }
    }
}