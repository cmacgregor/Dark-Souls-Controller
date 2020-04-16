using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface ICharacter
{
    ICharacterController characterController { get; set; }
    IActionController actionController { get; set; }
    ICharacterActions actions { get; set; }
}
