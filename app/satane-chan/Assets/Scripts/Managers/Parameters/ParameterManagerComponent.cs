using Assets.Scripts.Managers.Parameters.Bullets;
using Assets.Scripts.Managers.Parameters.Houses;
using Assets.Scripts.Managers.Parameters.Imaginations;
using Assets.Scripts.Managers.Parameters.Numbers;
using Assets.Scripts.Managers.Parameters.Obstacles;
using Assets.Scripts.Managers.Parameters.Players;
using Assets.Scripts.Managers.Parameters.Scores;
using UnityEngine;

namespace Assets.Scripts.Managers.Parameters
{
    /// <summary>
    /// パラメータ管理用コンポーネント
    /// </summary>
    public class ParameterManagerComponent : MonoBehaviour
    {
        /// <summary>
        /// 弾のパラメータ
        /// </summary>
        public Bullet bullet;
        /// <summary>
        /// 弾の壁
        /// </summary>
        public BulletWall bulletWall;
        /// <summary>
        /// スコア
        /// </summary>
        public Score score;
        /// <summary>
        /// 障害物
        /// </summary>
        public ShootingStar shootingStar;
        /// <summary>
        /// 障害物の壁
        /// </summary>
        public ShootingStarWall shootingStarWall;
        /// <summary>
        /// 吹き出し
        /// </summary>
        public Imagination imagination;
        /// <summary>
        /// 吹き出しの壁
        /// </summary>
        public ImaginationWall imaginationWall;
        /// <summary>
        /// 家
        /// </summary>
        public House house;
        /// <summary>
        /// HP
        /// </summary>
        public Hp hp;
        /// <summary>
        /// プレイヤー
        /// </summary>
        public Player player;
        /// <summary>
        /// プレイヤーの移動制限
        /// </summary>
        public PlayerWall playerWall;
        /// <summary>
        /// 数字
        /// </summary>
        public Number number;
    }

}
