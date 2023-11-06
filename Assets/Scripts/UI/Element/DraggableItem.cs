using System;
using System.Collections;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using Player;
using Tower;
using UI.Forms;
using UnityEngine;

namespace UI.Element
{
    public class DraggableItem : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _timeDragTrigger;

        private BaseMinion _minion;
        private Camera _camera;
        private Vector3 _prefMousePosition;
        private Vector3 _mouseDelta;
        private RaycastHit2D _currentHit;

        private Transform _selected;
        private bool _dragTrigger;
        private bool _isDragEntity;
        private UpgradeMinions _panel;
        private TowerSpawner _selectSpawner;
        private TowerSpawner _currentSpawner;
        private TowerStaticData _data;
        private Inventory _playerInventory;

        public void Construct(UpgradeMinions panel, Inventory inventory)
        {
            _panel = panel;
            _playerInventory = inventory;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            DragAndDropObject();

            if (Input.GetMouseButtonDown(0))
            {
                SelectPart();
            }

            if (Input.GetMouseButtonUp(0))
            {
                UpMouse();
            }
        }

        private void SelectPart()
        {
            Vector2 clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Single.PositiveInfinity, _layerMask);
            _currentHit = hit;

            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent(out BaseMinion minion))
                {
                    _minion = hit.collider.GetComponent<BaseMinion>();
                    _selected = hit.transform;
                    _selected.transform.position = clickPosition;

                    _currentSpawner = hit.transform.parent.GetComponent<TowerSpawner>();
                    _data = _currentSpawner.Data;

                    _playerInventory.CurrentData(_data);
                    _playerInventory.SetSpawnPosition(_currentSpawner);

                    hit.collider.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.8f, 0.8f);
                    hit.transform.GetComponent<Collider2D>().enabled = false;
                    StartCoroutine(DragTrigger());
                }
            }
        }

        private void DragAndDropObject()
        {
            var currentMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;
            _mouseDelta = currentMousePosition - _prefMousePosition;

            if (_selected != null)
            {
                if (_isDragEntity)
                {
                    _selected.position += new Vector3(_mouseDelta.x, _mouseDelta.y, 0);
                }
            }

            _prefMousePosition = currentMousePosition;
        }

        private void UpMouse()
        {
            if (_selected != null)
            {
                _isDragEntity = true;
                if (_dragTrigger)
                {
                    Vector2 clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D[] hitInfo = Physics2D.RaycastAll(clickPosition, Vector2.zero);
                    bool isTowerSpawner = false;
                    RaycastHit2D currentHit = new RaycastHit2D();

                    foreach (RaycastHit2D hit2D in hitInfo)
                    {
                        if (hit2D.collider.TryGetComponent(out TowerSpawner spawner))
                        {
                            if (!spawner.IsTowerCreated)
                            {
                                isTowerSpawner = true;
                                currentHit = hit2D;
                            }
                        }
                    }

                    if (isTowerSpawner == true)
                    {
                        NewPosition(currentHit);
                    }
                    else
                    {
                        ReturnToCurrentPosition();
                    }
                }
                else
                {
                    ReturnToCurrentPosition();
                    _panel.gameObject.SetActive(true);
                    _panel.PanelMinions.SetItemIcon(_minion);
                    _panel.UpgradeData(_data);
                    _panel.ShowMinions(_data);
                    _panel.MaxLevelMinions(_data);
                }
            }
        }

        private IEnumerator DragTrigger()
        {
            _dragTrigger = false;
            yield return new WaitForSeconds(_timeDragTrigger);
            _dragTrigger = true;
            _isDragEntity = true;
        }

        private void ReturnToCurrentPosition()
        {
            _currentHit.transform.localPosition = Vector3.zero;
            _currentHit.transform.GetComponent<Collider2D>().enabled = true;
            _currentHit.collider.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            _selected = null;
        }

        private void NewPosition(RaycastHit2D hitInfo)
        {
            _selected.SetParent(hitInfo.collider.transform, true);
            _selected.transform.localPosition = Vector3.zero;
            _currentHit.transform.GetComponent<Collider2D>().enabled = true;
            _currentHit.collider.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            _selected = null;
            _currentSpawner.IsCreateTower();
            _selectSpawner = hitInfo.collider.transform.GetComponent<TowerSpawner>();
            _selectSpawner.IsCreateTower();
            _selectSpawner.ChildMinion(_currentSpawner);
            _currentSpawner.ObjectOffset(_currentSpawner);
        }
    }
}