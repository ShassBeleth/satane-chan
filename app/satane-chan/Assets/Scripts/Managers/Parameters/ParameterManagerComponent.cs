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
    /// �p�����[�^�Ǘ��p�R���|�[�l���g
    /// </summary>
    public class ParameterManagerComponent : MonoBehaviour
    {
        /// <summary>
        /// �e�̃p�����[�^
        /// </summary>
        public Bullet bullet;
        /// <summary>
        /// �e�̕�
        /// </summary>
        public BulletWall bulletWall;
        /// <summary>
        /// �X�R�A
        /// </summary>
        public Score score;
        /// <summary>
        /// ��Q��
        /// </summary>
        public ShootingStar shootingStar;
        /// <summary>
        /// ��Q���̕�
        /// </summary>
        public ShootingStarWall shootingStarWall;
        /// <summary>
        /// �����o��
        /// </summary>
        public Imagination imagination;
        /// <summary>
        /// �����o���̕�
        /// </summary>
        public ImaginationWall imaginationWall;
        /// <summary>
        /// ��
        /// </summary>
        public House house;
        /// <summary>
        /// HP
        /// </summary>
        public Hp hp;
        /// <summary>
        /// �v���C���[
        /// </summary>
        public Player player;
        /// <summary>
        /// �v���C���[�̈ړ�����
        /// </summary>
        public PlayerWall playerWall;
        /// <summary>
        /// ����
        /// </summary>
        public Number number;
    }

}
