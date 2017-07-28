using System;
using System.Collections.Generic;

namespace Football
{
	/// <summary>
	/// 队伍
	/// </summary>
	public class Team : Entity
	{
		public Team ()
		{
		}

		void Start() {
			AddAgent<TeamAI> ();
		}

		/// <summary>
		/// 添加队员
		/// </summary>
		/// <param name="player">Player.</param>
		public void AddPlayer(Player player)
		{
			if (player == null) {
				return;
			}

			this.AddChild (player);
		}

		/// <summary>
		/// 移除队员
		/// </summary>
		/// <param name="player">Player.</param>
		public void RemovePlayer(Player player)
		{
			if (player == null) {
				return;
			}

			RemovePlayer (player.ID);
		}

		/// <summary>
		/// 移除队员
		/// </summary>
		/// <param name="playerID">Player I.</param>
		public void RemovePlayer(int playerID) {
			Player player = this.FindChildByID<Player> (playerID);
			if (player == null) {
				return;
			}

			this.RemoveChild (player);
		}

		/// <summary>
		/// 查找球员
		/// </summary>
		/// <returns>The play.</returns>
		/// <param name="playerID">Player I.</param>
		public Player FindPlay(int playerID) {
			return this.FindChildByID<Player> (playerID);
		}

		/// <summary>
		/// 替换球员
		/// </summary>
		/// <param name="playerID">Player I.</param>
		/// <param name="newPlayer">New player.</param>
		public void Replace(int playerID, Player newPlayer) {
			this.RemovePlayer (playerID);
			this.AddPlayer (newPlayer);
		}
	}
}

