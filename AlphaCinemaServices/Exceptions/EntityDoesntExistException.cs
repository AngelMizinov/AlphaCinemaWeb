using System;

namespace AlphaCinemaWeb.Exceptions
{
    public class EntityDoesntExistException : Exception
	{
        public EntityDoesntExistException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}