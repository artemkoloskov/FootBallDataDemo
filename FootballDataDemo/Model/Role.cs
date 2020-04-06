using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Нумератор для лучше читаемости кода
    /// </summary>
    public enum RoleType
    {
        NoRole = 0,
        Goalkeeper,
        Defender,
        Attacker
    }

    /// <summary>
    /// Класс роли игрока
    /// </summary>
    public class Role
    {
        public int Id { get; set; }

        public virtual RoleType RoleType { get; set; }
        
        /// <summary>
        /// Перевод нумератора типов ролей в читаемую форму для пользователя
        /// </summary>
        public string Title
        {
            get
            {
                string title = "";

                switch (RoleType)
                {
                    case RoleType.Goalkeeper:
                        title = "Вратарь";
                        return title;
                    case RoleType.Defender:
                        title = "Защитник";
                        return title;
                    case RoleType.Attacker:
                        title = "Нападающий";
                        return title;
                    case RoleType.NoRole:
                        title = "Нет роли";
                        break;
                }

                return title;
            }
        }

        public Role ()
        {

        }
    }
}
