﻿namespace modLib.Entities.DTO.Game
{
    public class UpdateGameDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Version { get; set; }
    }
}
