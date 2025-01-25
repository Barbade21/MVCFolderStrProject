namespace MVCFolderStrProjectLibrary
{
    public class User
    {

       // [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        //[Required(ErrorMessage = "User Name is required.")]
        //[StringLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        //[StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address { get; set; }

       // [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

      //  [Required(ErrorMessage = "Country selection is required.")]
        public int CountryId { get; set; }

        public string CountryName { get; set; } // Optional for display purposes

       // [Required(ErrorMessage = "State selection is required.")]
        public int StateId { get; set; }

        public string StateName { get; set; } // Optional for display purposes

       // [Required(ErrorMessage = "City selection is required.")]
        public int CityId { get; set; }

        public string CityName { get; set; } // Optional for display purposes

    }
}
