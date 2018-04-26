using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace LevelEditor
{

    //Code for All Level Creation/Editing
    public class LevelCreator : MonoBehaviour
    {
        LevelManager manager;
        GridBase gridBase;
        InterfaceManager ui;
        NumberTrackers numberTracker;
        
        public float totalMoney;

        Vector3 mousePosition;
        Vector3 fencePosition;
        Vector3 worldPosition;

        //place object variables
        bool objectHas;
        GameObject objectToPlace;
        GameObject objectClone;
        Level_Object objectProperties;
        bool objectDelete;

        //place Building variables
        bool buildingHas;
        GameObject buildingToPlace;
        GameObject buildingClone;
        Building_Object buildingProperties;

        bool buildingFenceDelete;

        //place fence variables
        bool fenceHas;
        GameObject fenceToPlace;
        GameObject fenceToPlace2;
        GameObject fenceClone;
        Level_Object fenceProperties;
        FenceClass fenceFenceClass;


        //place Animal variables
        bool animalHas;
        GameObject animalToPlace;
        GameObject animalClone;
        Level_Object animalProperties;
        AnimalClass animalAnimalClass;
        bool animalDelete;

    

        //Terrain variable
        bool hasMaterial;
        bool paintTile;
        GameObject terrainToPlace;
        Terrain_Object terrainProperties;
        public Material matToPlace;
        Node previousNode;
        Material prevMaterial;
        Quaternion targetRot;
        Quaternion prevRotation;

        Color prevColor;
        /*//Wall creator variables
        bool createWall;
        
        GameObject wallObject;
        Node startNode_Wall;
        Node endNodeWall;
        // public Material[] wallPlacementMat;
        bool deleteWall;
        */

        //place ecnlsoure variables
        bool hasEnclosure;
        GameObject enclosureToPlace;
        GameObject cloneEnclosure;
        Level_Object enclosureProperties;
        bool deleteEnclosure;
        public GameObject enclosureObject;
        public GameObject[] enclosure;
        int totalPlaced;
        GameObject actualWallPlaced;
        GameObject actualCornerPlaced;
        public GameObject wallPrefab;
        public GameObject wallCornerPrefab;

        Vector3 nodePosN;
        Vector3 nodePosS;
        Vector3 nodePosE;
        Vector3 nodePosW;

        Vector3 nodePosNE;
        Vector3 nodePosSE;
        Vector3 nodePosNW;
        Vector3 nodePosSW;

        bool cornerCheck;

        RaycastHit hit;

        public GameObject[] fences;

        float noFences = 0;

        public FenceNode wallBuildArea;

        int matId;
        public GameObject fencePrefab;
        public GameObject[] fenceMarkers;
        public Toggle fillAll;

        public Animator animCare;
        public Animator animCost;
        public Animator animEnrichment;
        public Animator animFences;
        public Animator animTerrain;

        StarTracking starTracking;

        int noWater;
        GameObject inGameToggle;
        public GameObject terrainObject;
        public GameObject terrainObject1;
        public GameObject terrainObject2;
        public GameObject terrainObject3;

        public GameObject containsBuilding;

        public Image ErrorMessageOIW;
        public Image ErrorMessageWIW;
        public Image ErrorMessage2mny;
        public Image ErrorMessageIW;

        public AudioSource errorAudioSource;

        bool objectInWay = false;
        bool waterInWay = false;
        bool tooManyBuild = false;
        bool placeInWater = false;

        void ErrorMessage()
        {
            if (objectInWay == true)
            {
                ErrorMessageOIW.gameObject.SetActive(true);
                errorAudioSource.Play();
            }
            else
            {
                ErrorMessageOIW.gameObject.SetActive(false);
            }
            if (waterInWay == true)
            {
                ErrorMessageWIW.gameObject.SetActive(true);
                errorAudioSource.Play();
            }
            else
            {
                ErrorMessageWIW.gameObject.SetActive(false);
            }
            if (tooManyBuild == true)
            {
                ErrorMessage2mny.gameObject.SetActive(true);
                errorAudioSource.Play();
            }
            else
            {
                ErrorMessage2mny.gameObject.SetActive(false);
            }
            if (placeInWater == true)
            {
                ErrorMessageIW.gameObject.SetActive(true);
                errorAudioSource.Play();
            }
            else
            {
                ErrorMessageIW.gameObject.SetActive(false);
            }
        }

        void Start()
        {
            gridBase = GridBase.GetInstance();
            manager = LevelManager.GetInstance();
            ui = InterfaceManager.GetInstance();
            starTracking = StarTracking.GetInstance();
            inGameToggle = GameObject.Find("Toggle");
        }

        void Update()
        {
            PlaceObject();
            DeleteObject();

            PlaceBuilding();
            DeleteBuildingorFence();

            PlaceFence();
           
            PlaceAnimal();
            DeleteAnimal();
   
            PaintTile();

        }

        public void CloseAll()
        {
            objectHas = false;
            objectDelete = false;

            buildingHas = false;
            buildingFenceDelete = false;

            fenceHas = false;
           
            animalHas = false;
            animalDelete = false;

            deleteEnclosure = false;
            hasEnclosure = false;

            paintTile = false;
            hasMaterial = false;

        }


        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                mousePosition = hit.point;
            }
        }

        IEnumerator objectInWayFeedBack()
        {
            Renderer r = null;

            if (objectClone != null)
            {
              r = objectClone.GetComponentInChildren<Renderer>();
            }
            if (buildingClone != null)
            {
             r = buildingClone.GetComponentInChildren<Renderer>();
            }

            
            r.material.color = Color.red;
            ErrorMessageOIW.gameObject.SetActive(true);
            errorAudioSource.Play();
            yield return new WaitForSeconds(0.7f);
            r.material.color = Color.white;
            ErrorMessageOIW.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);

        }

        IEnumerator waterInWayFeedBack()
        {
            Renderer r = null;

            if (objectClone != null)
            {
                r = objectClone.GetComponentInChildren<Renderer>();
            }
            if (buildingClone != null)
            {
                r = buildingClone.GetComponentInChildren<Renderer>();
            }

            
            r.material.color = Color.red;
            ErrorMessageWIW.gameObject.SetActive(true);
            errorAudioSource.Play();
            yield return new WaitForSeconds(0.7f);
            r.material.color = Color.white;
            ErrorMessageWIW.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        IEnumerator tooManyBuildsFeedBack()
        {
            Renderer r = null;

            if (objectClone != null)
            {
                r = objectClone.GetComponentInChildren<Renderer>();
            }
            if (buildingClone != null)
            {
                r = buildingClone.GetComponentInChildren<Renderer>();
            }

           
            r.material.color = Color.red;
            ErrorMessage2mny.gameObject.SetActive(true);
            errorAudioSource.Play();
            yield return new WaitForSeconds(0.7f);
            r.material.color = Color.white;
            ErrorMessage2mny.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        IEnumerator placeInWaterFeedBack()
        {
            Renderer r = null;
            if (objectClone != null)
            {
                r = objectClone.GetComponentInChildren<Renderer>();
            }
            if (buildingClone != null)
            {
                r = buildingClone.GetComponentInChildren<Renderer>();
            }
           
            r.material.color = Color.red;
            ErrorMessageIW.gameObject.SetActive(true);
            errorAudioSource.Play();
            yield return new WaitForSeconds(0.7f);
            r.material.color = Color.white;
            ErrorMessageIW.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        // Object Placement for all areas
        //Currently all work the exact same 
        //changes to be made to way fence and animals are placed

        #region Place Objects
        void PlaceObject()
        {
            if (objectHas)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (!ui.mouseOverUIElement && curNode != null)
                {
                    worldPosition = curNode.vis.transform.position;
                }

                /*if (curNode != null && !ui.mouseOverUIElement)
                {
                    if (previousNode == null)
                    {
                        previousNode = curNode;
                        prevColor = Color.white;
                    }
                    else
                    {
                        if (previousNode != curNode)
                        {
                            if (curNode.placedObj == null)
                            {
                                curNode.tileRenderer.material.color = Color.green;
                            }
                            else
                            {
                                curNode.tileRenderer.material.color = Color.red;
                            }
                            previousNode.tileRenderer.material.color = Color.white;
                        }
                        previousNode = curNode;
                        prevColor = curNode.tileRenderer.material.color;
                    }
                }*/
                if (objectClone == null)
                {
                    objectClone = Instantiate(objectToPlace, worldPosition, Quaternion.identity) as GameObject;
                    objectProperties = objectClone.GetComponent<Level_Object>();
                }
                else
                {
                    objectClone.transform.position = worldPosition;
                    if (Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        if (hit.collider.tag == "EnclosureMarker")
                        {
                            if (curNode.placedObj != null || curNode.placedBuild != null)
                            {
                                StartCoroutine("objectInWayFeedBack");
                                
                            }
                            else
                            {
                                
                                //Water current texture id = 3, be sure to change when rearranging textures
                                if (curNode.vis.GetComponent<NodeObject>().textureid == 3 && objectProperties.isWaterObject == true)
                                {
                                    placingObjects();
                                }
                                else if (curNode.vis.GetComponent<NodeObject>().textureid == 3 && objectProperties.isWaterObject == false)
                                {
                                    StartCoroutine("waterInWayFeedBack");
                                    
                                }
                                else if (curNode.vis.GetComponent<NodeObject>().textureid != 3 && objectProperties.isWaterObject == false)
                                {
                                    placingObjects();
                                }
                                else if (curNode.vis.GetComponent<NodeObject>().textureid != 3 && objectProperties.isWaterObject == true)
                                {
                                    StartCoroutine("placeInWaterFeedBack");
                                    
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Must be placed in Enclosure");
                        }
                    }

                    //Rotation
                    /*
                    if (Input.GetMouseButtonDown(1))
                    {
                        objectProperties.ChangeRotation();
                    }
                    */
                }
            }
            else
            {
                if (objectClone != null)
                {
                    Destroy(objectClone);
                }
            }
        }

       
        public void PassObjectToPlace(string objectId)
        {
            if (objectClone != null)
            {
                Destroy(objectClone);
            }

            CloseAll();
            objectHas = true;
            objectClone = null;
            objectToPlace = ResourcesManager.GetInstance().GetObjectBase(objectId).objectPrefab;
        }
        void DeleteObject()
        {
            if (objectDelete)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        deletingObjects();
                    }
                }
            }
        }
        public void DeleteObjects()
        {
            CloseAll();
            objectDelete = true;
        }

        void deletingObjects()
        {
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            Level_Object objectPlacedProperties = curNode.placedObj.gameObject.GetComponent<Level_Object>();
            switch(objectPlacedProperties.objectType)
            {
                case Level_Object.ObjectType.ANIMALOBJECT:
                    AnimalClass animalEnumComponents = curNode.placedObj.gameObject.GetComponent<AnimalClass>();
                    switch (animalEnumComponents.animalFoodType)
                    {
                        case AnimalClass.AnimalFoodTypes.Carnivore:
                            NumberTrackers.noAnimalsC--;
                            break;

                        case AnimalClass.AnimalFoodTypes.Herbivore:
                            NumberTrackers.noAnimalsH--;
                            break;

                        case AnimalClass.AnimalFoodTypes.Omnivore:
                            NumberTrackers.noAnimalsO--;
                            break;
                    }

                    //Switch currently not need but implemented for furture addition of other animals
                    switch (animalEnumComponents.animalType)
                    {
                        case AnimalClass.AnimalTypes.PENGUIN:
                            NumberTrackers.noAnimals--;
                            break;
                            
                        case AnimalClass.AnimalTypes.OTHER:
                            NumberTrackers.noAnimals--;
                            break;
                    }


                    break;

                case Level_Object.ObjectType.ENRICHMENTOBJECT:
                   ToyClass toyEnumComponents = curNode.placedObj.gameObject.GetComponent<ToyClass>();
                    //Subtract from No. trackers based on enum
                    switch (toyEnumComponents.toyType)
                    {
                        case ToyClass.ToyTypes.TOY:
                            NumberTrackers.noToys--;
                            break;
                        case ToyClass.ToyTypes.WATERTOY:
                            NumberTrackers.noWaterToys--;
                            break;
                    }
                    break;
                case Level_Object.ObjectType.FOLIAGEOBJECT:
                    //get the Enum before Deleting
                    FoliageClass foliageEnumComponents = curNode.placedObj.gameObject.GetComponent<FoliageClass>();
                    //Subtract from No. trackers based on enum
                    switch (foliageEnumComponents.foliageType)
                           {
                                case FoliageClass.FoliageTypes.BUSH:
                                    NumberTrackers.noBush--;
                                    break;
                                case FoliageClass.FoliageTypes.ROCK:
                                    NumberTrackers.noRock--;
                                    break;
                                case FoliageClass.FoliageTypes.OTHER:
                                    NumberTrackers.noOther--;
                                    break;
                            }
                    break;

                case Level_Object.ObjectType.FOODOBJECT:
                    CareClass careEnumComponent = curNode.placedObj.gameObject.GetComponent<CareClass>();
                    switch (careEnumComponent.foodType)
                    {
                        case CareClass.FoodType.CARNIVOUROUS:
                            NumberTrackers.noCarnivorous--;
                            break;
                        case CareClass.FoodType.HERBIVOROUS:
                            NumberTrackers.noHerbivorous--;
                            break;
                    }
                    break;
            }

            NumberTrackers.totalMoney += objectPlacedProperties.price;
            NumberTrackers.maintenance -= objectPlacedProperties.maintenance;

            starTracking.CareCheck(animCare);
            starTracking.CostCheck(animCost);
            starTracking.EnrichmentCheck(animEnrichment);

            manager.inSceneObject.Remove(curNode.placedObj.gameObject);
            Destroy(curNode.placedObj.gameObject);
            curNode.placedObj = null;

            
        }
        void placingObjects()
        {

            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
            GameObject objectActualPlaced = Instantiate(objectToPlace, worldPosition, objectClone.transform.rotation) as GameObject;
            Level_Object objectPlacedProperties = objectActualPlaced.GetComponent<Level_Object>();
           

            objectPlacedProperties.gridPosX = curNode.nodePosX;
            objectPlacedProperties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = objectPlacedProperties;
            manager.inSceneObject.Add(objectActualPlaced);

            NumberTrackers.totalMoney -= objectPlacedProperties.price;
            NumberTrackers.maintenance += objectPlacedProperties.maintenance;

            switch (objectPlacedProperties.objectType)
            {
                case Level_Object.ObjectType.ANIMALOBJECT:
                    AnimalClass animalEnumComponents = curNode.placedObj.gameObject.GetComponent<AnimalClass>();
                    switch (animalEnumComponents.animalFoodType)
                    {
                        case AnimalClass.AnimalFoodTypes.Carnivore:
                            NumberTrackers.noAnimalsC++;
                            break;

                        case AnimalClass.AnimalFoodTypes.Herbivore:
                            NumberTrackers.noAnimalsH++;
                            break;

                        case AnimalClass.AnimalFoodTypes.Omnivore:
                            NumberTrackers.noAnimalsO++;
                            break;
                    }

                    //Switch currently not need but implemented for furture addition of other animals
                    switch (animalEnumComponents.animalType)
                    {
                        case AnimalClass.AnimalTypes.PENGUIN:
                            NumberTrackers.noAnimals++;
                            break;

                        case AnimalClass.AnimalTypes.OTHER:
                            NumberTrackers.noAnimals++;
                            break;
                    }
                    break;
                case Level_Object.ObjectType.ENRICHMENTOBJECT:
                    ToyClass toyEnumComponents = curNode.placedObj.gameObject.GetComponent<ToyClass>();
                    //Subtract from No. trackers based on enum
                    switch (toyEnumComponents.toyType)
                    {
                        case ToyClass.ToyTypes.TOY:
                            NumberTrackers.noToys++;
                            break;
                        case ToyClass.ToyTypes.WATERTOY:
                            NumberTrackers.noWaterToys++;
                            break;
                    }
                    break;
                case Level_Object.ObjectType.FOLIAGEOBJECT:
                    //get the Enum before Deleting
                    FoliageClass foliageEnumComponents = curNode.placedObj.gameObject.GetComponent<FoliageClass>();
                    //Subtract from No. trackers based on enum
                    switch (foliageEnumComponents.foliageType)
                    {
                        case FoliageClass.FoliageTypes.BUSH:
                            NumberTrackers.noBush++;
                            break;
                        case FoliageClass.FoliageTypes.ROCK:
                            NumberTrackers.noRock++;
                            break;
                        case FoliageClass.FoliageTypes.OTHER:
                            NumberTrackers.noOther++;
                            break;
                    }
                    break;

                case Level_Object.ObjectType.FOODOBJECT:
                    CareClass careEnumComponent = curNode.placedObj.gameObject.GetComponent<CareClass>();
                    switch (careEnumComponent.foodType)
                    {
                        case CareClass.FoodType.CARNIVOUROUS:
                            NumberTrackers.noCarnivorous++;
                            break;
                        case CareClass.FoodType.HERBIVOROUS:
                            NumberTrackers.noHerbivorous++;
                            break;
                    }
                    break;
            }
            starTracking.CareCheck(animCare);
            starTracking.CostCheck(animCost);
            starTracking.EnrichmentCheck(animEnrichment);
        }


        #endregion

        #region Place Building
        void PlaceBuilding()
        {
            if (buildingHas)
            {
                UpdateMousePosition();
     
                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
               
                Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

                Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
                Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));

                if (!ui.mouseOverUIElement && curNode != null)
                {
                    worldPosition = curNode.vis.transform.position;
                }

               /* if (previousNode == null)
                {
                    previousNode = curNode;
                    prevColor = Color.white;
                }
                else
                {
                    if (previousNode != curNode)
                    {
                        if (curNode.placedObj == null)
                        {
                            curNode.tileRenderer.material.color = Color.green;
                        }
                        else
                        {
                            curNode.tileRenderer.material.color = Color.red;
                        }
                        previousNode.tileRenderer.material.color = Color.white;
                    }
                    previousNode = curNode;
                    prevColor = curNode.tileRenderer.material.color;
                }
                */
                if (buildingClone == null)
                {
                    buildingClone = Instantiate(buildingToPlace, worldPosition, Quaternion.identity) as GameObject;
                    buildingProperties = buildingClone.GetComponent<Building_Object>();
                }
                else
                {
                    buildingClone.transform.position = worldPosition;

                    if (Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        if (hit.collider.tag == "EnclosureMarker")
                        {
                             if(buildingProperties.buildingType == Building_Object.BuildingType.AID && NumberTrackers.noAid > 0)
                            {
                                StartCoroutine("tooManyBuildsFeedBack");
                                
                            }
                            else if (buildingProperties.buildingType == Building_Object.BuildingType.SHELTER && NumberTrackers.noShelter > 0)
                            {
                                StartCoroutine("tooManyBuildsFeedBack");
                                
                            }
                            else if (curNode.placedObj != null || nodeE.placedObj != null || nodeS.placedObj != null || nodeW.placedObj != null || nodeN.placedObj != null || nodeNE.placedObj != null || nodeNW.placedObj != null || nodeSE.placedObj != null || nodeSW.placedObj != null||curNode.placedBuild != null || nodeE.placedBuild != null || nodeS.placedBuild != null || nodeW.placedBuild != null || nodeN.placedBuild != null || nodeNE.placedBuild != null || nodeNW.placedBuild != null || nodeSE.placedBuild != null || nodeSW.placedBuild != null)
                            {
                                StartCoroutine("objectInWayFeedBack");
                               
                            }
                            else
                            {
                                //Water current texture id = 3, be sure to change when rearranging textures
                                if (curNode.vis.GetComponent<NodeObject>().textureid != 3 && nodeN.vis.GetComponent<NodeObject>().textureid != 3 && nodeE.vis.GetComponent<NodeObject>().textureid != 3 && nodeW.vis.GetComponent<NodeObject>().textureid != 3 && nodeS.vis.GetComponent<NodeObject>().textureid != 3 && nodeNE.vis.GetComponent<NodeObject>().textureid != 3 && nodeNW.vis.GetComponent<NodeObject>().textureid != 3 && nodeSE.vis.GetComponent<NodeObject>().textureid != 3 && nodeSW.vis.GetComponent<NodeObject>().textureid != 3)
                                {
                                    placingBuildings();
                                    buildingHas = false;
                                }
                                else if (curNode.vis.GetComponent<NodeObject>().textureid == 3 || nodeN.vis.GetComponent<NodeObject>().textureid == 3 || nodeE.vis.GetComponent<NodeObject>().textureid == 3 || nodeW.vis.GetComponent<NodeObject>().textureid == 3 || nodeS.vis.GetComponent<NodeObject>().textureid == 3 || nodeNE.vis.GetComponent<NodeObject>().textureid == 3 || nodeNW.vis.GetComponent<NodeObject>().textureid == 3 || nodeSE.vis.GetComponent<NodeObject>().textureid == 3 || nodeSW.vis.GetComponent<NodeObject>().textureid == 3)
                                {
                                    StartCoroutine("waterInWayFeedBack");
                                   
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Must be placed in Enclosure");
                        }
                    }

                    //Rotation
                    /*
                    if (Input.GetMouseButtonDown(1))
                    {
                        objectProperties.ChangeRotation();
                    }
                    */
                }
            }
            else
            {
                if (buildingClone != null)
                {
                    Destroy(buildingClone);
                }
            }
        }

        public void PassBuildingToPlace(string buildingId)
        {
            if (buildingClone != null)
            {
                Destroy(buildingClone);
            }

            CloseAll();
            buildingHas = true;
            buildingClone = null;
            buildingToPlace = ResourcesManager.GetInstance().GetBuildingBase(buildingId).buildingPrefab;
        }
        
        void DeleteBuildingorFence()
        {

            if (buildingFenceDelete)
            {

                UpdateMousePosition();
               
                    Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
                    Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
                    Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
                    Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
                    Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

                    Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                    Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                    Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
                    Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
                    Node centerNode = null;



                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                    {
                        if (hit.collider.tag != "fence")
                        {
                            if (curNode.placedBuild != null)
                            {


                                if (nodeN.placedBuild != null && nodeN.placedBuild.center == true)
                                {
                                    centerNode = nodeN;

                                }
                                else if (nodeS.placedBuild != null && nodeS.placedBuild.center == true)
                                {
                                    centerNode = nodeS;

                                }
                                else if (nodeE.placedBuild != null && nodeE.placedBuild.center == true)
                                {
                                    centerNode = nodeE;

                                }
                                else if (nodeW.placedBuild != null && nodeW.placedBuild.center == true)
                                {
                                    centerNode = nodeW;

                                }
                                else if (nodeNE.placedBuild != null && nodeNE.placedBuild.center == true)
                                {
                                    centerNode = nodeNE;

                                }
                                else if (nodeSE.placedBuild != null && nodeSE.placedBuild.center == true)
                                {
                                    centerNode = nodeSE;

                                }
                                else if (nodeNW.placedBuild != null && nodeNW.placedBuild.center == true)
                                {
                                    centerNode = nodeNW;
                                }
                                else if (nodeSW.placedBuild != null && nodeSW.placedBuild.center == true)
                                {
                                    centerNode = nodeSW;

                                }
                                else if (curNode.placedBuild != null && curNode.placedBuild.center == true)
                                {
                                    centerNode = curNode;
                                }
                                else
                                {
                                    Debug.Log("rawr");
                                }

                            }

                            if (centerNode != null)
                            {
                                Building_Object buildingPlacedProperties = centerNode.placedBuild.gameObject.GetComponent<Building_Object>();
                                Node newNodeN = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x, 0, centerNode.placedBuild.transform.localPosition.z + (1 * gridBase.offset)));
                                Node newNodeS = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x, 0, centerNode.placedBuild.transform.localPosition.z - (1 * gridBase.offset)));
                                Node newNodeE = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x + (1 * gridBase.offset), 0, centerNode.placedBuild.transform.localPosition.z));
                                Node newNodeW = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x - (1 * gridBase.offset), 0, centerNode.placedBuild.transform.localPosition.z));
                                Node newNodeNE = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x + (1 * gridBase.offset), 0, centerNode.placedBuild.transform.localPosition.z + (1 * gridBase.offset)));
                                Node newNodeSE = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x + (1 * gridBase.offset), 0, centerNode.placedBuild.transform.localPosition.z - (1 * gridBase.offset)));
                                Node newNodeNW = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x - (1 * gridBase.offset), 0, centerNode.placedBuild.transform.localPosition.z + (1 * gridBase.offset)));
                                Node newNodeSW = gridBase.NodeFromWorldPosition(new Vector3(centerNode.placedBuild.transform.localPosition.x - (1 * gridBase.offset), 0, centerNode.placedBuild.transform.localPosition.z - (1 * gridBase.offset)));


                                switch (buildingPlacedProperties.buildingType)
                                {
                                    case Building_Object.BuildingType.AID:
                                        NumberTrackers.noAid--;
                                        break;
                                    case Building_Object.BuildingType.SHELTER:
                                        NumberTrackers.noShelter--;
                                        break;
                                }
                                NumberTrackers.totalMoney += buildingPlacedProperties.price;
                                NumberTrackers.maintenance -= buildingPlacedProperties.maintenance;

                                starTracking.CareCheck(animCare);
                                starTracking.CostCheck(animCost);

                                manager.inSceneBuilding.Remove(centerNode.placedBuild.gameObject);
                                Destroy(centerNode.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeE.placedBuild.gameObject);
                                Destroy(newNodeE.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeW.placedBuild.gameObject);
                                Destroy(newNodeW.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeS.placedBuild.gameObject);
                                Destroy(newNodeS.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeN.placedBuild.gameObject);
                                Destroy(newNodeN.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeNE.placedBuild.gameObject);
                                Destroy(newNodeNE.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeSE.placedBuild.gameObject);
                                Destroy(newNodeSE.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeSW.placedBuild.gameObject);
                                Destroy(newNodeSW.placedBuild.gameObject);

                                manager.inSceneBuilding.Remove(newNodeNW.placedBuild.gameObject);
                                Destroy(newNodeNW.placedBuild.gameObject);



                                newNodeE.placedBuild = null;
                                newNodeN.placedBuild = null;
                                newNodeS.placedBuild = null;
                                newNodeW.placedBuild = null;
                                newNodeNE.placedBuild = null;
                                newNodeSE.placedBuild = null;
                                newNodeNW.placedBuild = null;
                                newNodeSW.placedBuild = null;
                                centerNode.placedBuild = null;
                            }

                        }
                        //Delete fence
                        else if (hit.collider.tag == "fence")
                        {
                            FenceNode wallBuildArea = hit.collider.GetComponentInParent<FenceNode>();
                            Renderer r = wallBuildArea.GetComponent<Renderer>();
                            r.material.color = Color.white;
                            GameObject fenceActualPlaced = hit.collider.GetComponent<GameObject>();
                            FenceClass fenceEnumComponent = hit.collider.GetComponent<FenceClass>(); //get the Enum
                            Level_Object fencePlacedProperties = hit.collider.GetComponent<Level_Object>();

                            NumberTrackers.totalMoney += fencePlacedProperties.price;
                            NumberTrackers.maintenance -= fencePlacedProperties.maintenance;

                            switch (fenceEnumComponent.fenceType)
                            {
                                case FenceClass.FenceTypes.CONCRETE:
                                    NumberTrackers.noConcrete--;
                                    break;
                                case FenceClass.FenceTypes.CONCRETEW:
                                    NumberTrackers.noConcreteW--;
                                    break;
                                case FenceClass.FenceTypes.GLASS:
                                    NumberTrackers.noGlass--;
                                    break;
                                case FenceClass.FenceTypes.WIRE:
                                    NumberTrackers.noWire--;
                                    break;
                                case FenceClass.FenceTypes.WOODEN:
                                    NumberTrackers.noWooden--;
                                    break;
                                case FenceClass.FenceTypes.WOODENW:
                                    NumberTrackers.noWoodenW--;
                                    break;
                            }

                            starTracking.FencesCheck(animFences);
                            starTracking.CostCheck(animCost);

                            wallBuildArea.fenceObj = false;
                            manager.inSceneFences.Remove(hit.collider.gameObject);
                            Destroy(hit.collider.gameObject);
                        }
                       
                    }

            }
        }
        public void DeleteBuildingsorFence()
        {
            CloseAll();
            buildingFenceDelete = true;
        }

        void placingBuildings()
        {

            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));
    
            Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
            Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));

            GameObject buildingActualPlaced = Instantiate(buildingToPlace, worldPosition, buildingClone.transform.rotation) as GameObject;
            GameObject buildingN = Instantiate(containsBuilding, new Vector3 (nodeN.nodePosX * gridBase.offset, 0,nodeN.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            GameObject buildingE = Instantiate(containsBuilding, new Vector3(nodeE.nodePosX * gridBase.offset, 0, nodeE.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            GameObject buildingW = Instantiate(containsBuilding, new Vector3(nodeW.nodePosX * gridBase.offset, 0, nodeW.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            GameObject buildingS = Instantiate(containsBuilding, new Vector3(nodeS.nodePosX * gridBase.offset, 0, nodeS.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            GameObject buildingNE = Instantiate(containsBuilding, new Vector3(nodeNE.nodePosX * gridBase.offset, 0, nodeNE.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            GameObject buildingNW = Instantiate(containsBuilding, new Vector3(nodeNW.nodePosX * gridBase.offset, 0, nodeNW.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            GameObject buildingSE = Instantiate(containsBuilding, new Vector3(nodeSE.nodePosX * gridBase.offset, 0, nodeSE.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            GameObject buildingSW = Instantiate(containsBuilding, new Vector3(nodeSW.nodePosX * gridBase.offset, 0, nodeSW.nodePosZ * gridBase.offset), Quaternion.identity) as GameObject;
            //Center
            Building_Object buildingPlacedProperties = buildingActualPlaced.GetComponent<Building_Object>();
            buildingPlacedProperties.gridPosX = curNode.nodePosX;
            buildingPlacedProperties.gridPosZ = curNode.nodePosZ;
            buildingPlacedProperties.center = true;
            //North
            Building_Object buildingNorth = buildingN.GetComponent<Building_Object>();
            buildingNorth.gridPosX = nodeN.nodePosX;
            buildingNorth.gridPosZ = nodeN.nodePosZ;
            buildingNorth.center = false;
            //South
            Building_Object buildingSouth = buildingS.GetComponent<Building_Object>();
            buildingSouth.gridPosX = nodeS.nodePosX ;
            buildingSouth.gridPosZ = nodeS.nodePosZ;
            buildingSouth.center = false;
            //East
            Building_Object buildingEast = buildingE.GetComponent<Building_Object>();
            buildingEast.gridPosX = nodeE.nodePosX;
            buildingEast.gridPosZ = nodeE.nodePosZ;
            buildingEast.center = false;
            //West
            Building_Object buildingWest = buildingW.GetComponent<Building_Object>();
            buildingWest.gridPosX = nodeW.nodePosX ;
            buildingWest.gridPosZ = nodeW.nodePosZ ;
            buildingWest.center = false;

            //NorthE
            Building_Object buildingNorthE = buildingNE.GetComponent<Building_Object>();
            buildingNorthE.gridPosX = nodeNE.nodePosX ;
            buildingNorthE.gridPosZ = nodeNE.nodePosZ ;
            buildingNorthE.center = false;
            //SouthE
            Building_Object buildingSouthE = buildingSE.GetComponent<Building_Object>();
            buildingSouthE.gridPosX = nodeSE.nodePosX ;
            buildingSouthE.gridPosZ = nodeSE.nodePosZ;
            buildingSouthE.center = false;
            //NorthW
            Building_Object buildingNorthW = buildingNW.GetComponent<Building_Object>();
            buildingNorthW.gridPosX = nodeNW.nodePosX ;
            buildingNorthW.gridPosZ = nodeNW.nodePosZ;
            buildingNorthW.center = false;
            //SouthW
            Building_Object buildingSouthW = buildingSW.GetComponent<Building_Object>();
            buildingSouthW.gridPosX = nodeSW.nodePosX ;
            buildingSouthW.gridPosZ = nodeSW.nodePosZ ;
            buildingSouthW.center = false;

            curNode.placedBuild = buildingPlacedProperties;
            
            nodeN.placedBuild = buildingNorth;
            nodeS.placedBuild = buildingSouth;
            nodeW.placedBuild = buildingWest;
            nodeE.placedBuild = buildingEast;
            nodeNE.placedBuild = buildingNorthE;
            nodeSE.placedBuild = buildingSouthE;
            nodeNW.placedBuild = buildingNorthW;
            nodeSW.placedBuild = buildingSouthW;

            manager.inSceneBuilding.Add(buildingActualPlaced);
            manager.inSceneBuilding.Add(buildingN);
            manager.inSceneBuilding.Add(buildingS);
            manager.inSceneBuilding.Add(buildingW);
            manager.inSceneBuilding.Add(buildingE);
            manager.inSceneBuilding.Add(buildingNE);
            manager.inSceneBuilding.Add(buildingSE);
            manager.inSceneBuilding.Add(buildingNW);
            manager.inSceneBuilding.Add(buildingSW);

            switch(buildingPlacedProperties.buildingType)
                        {
                            case Building_Object.BuildingType.AID:
                                NumberTrackers.noAid++;
                                break;
                            case Building_Object.BuildingType.SHELTER:
                                NumberTrackers.noShelter++;
                                break;
                        }

            NumberTrackers.totalMoney -= buildingPlacedProperties.price;
            NumberTrackers.maintenance += buildingPlacedProperties.maintenance;
            starTracking.CareCheck(animCare);
            starTracking.CostCheck(animCost);
        }


        #endregion


        #region Place Fence
        void PlaceFence()
        {
            if (fenceHas)
            {
                UpdateMousePosition();
                
                if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    if (hit.collider.tag == "preplacedFence")
                    {
                        if (fillAll.isOn)
                        {
                            fenceMarkers = GameObject.FindGameObjectsWithTag("preplacedFence");
                            foreach (GameObject fence in fenceMarkers)
                            {
                                Renderer r = fence.GetComponent<Renderer>();
                                r.material.color = Color.blue;
                                FenceNode wallBuildArea = fence.GetComponent<FenceNode>();
                                if (wallBuildArea.fenceObj == false)
                                {
                                    FenceClass fenceEnumComponent = fenceToPlace.GetComponent<FenceClass>(); //get the Enum
                                    GameObject fenceActualPlaced = Instantiate(fenceToPlace, fence.transform.position, fence.transform.rotation) as GameObject;
                                    Level_Object fencePlacedProperties = fenceActualPlaced.GetComponent<Level_Object>();
                                    wallBuildArea.fenceObj = true;
                                    fenceActualPlaced.transform.parent = wallBuildArea.transform;
                                    manager.inSceneFences.Add(fenceActualPlaced);
                                    NumberTrackers.totalMoney -= fencePlacedProperties.price;
                                    NumberTrackers.maintenance += fencePlacedProperties.maintenance;

                                    switch (fenceEnumComponent.fenceType)
                                    {
                                        case FenceClass.FenceTypes.CONCRETE:
                                            NumberTrackers.noConcrete++;
                                            break;
                                        case FenceClass.FenceTypes.CONCRETEW:
                                            NumberTrackers.noConcreteW++;
                                            break;
                                        case FenceClass.FenceTypes.GLASS:
                                            NumberTrackers.noGlass++;
                                            break;
                                        case FenceClass.FenceTypes.WIRE:
                                            NumberTrackers.noWire++;
                                            break;
                                        case FenceClass.FenceTypes.WOODEN:
                                            NumberTrackers.noWooden++;
                                            break;
                                        case FenceClass.FenceTypes.WOODENW:
                                            NumberTrackers.noWoodenW++;
                                            break;
                                    }

                                    starTracking.CostCheck(animCost);
                                    starTracking.FencesCheck(animFences);
                                }
                            }
                        }
                        else if (!fillAll.isOn)
                        {
                            FenceNode wallBuildArea = hit.collider.gameObject.GetComponent<FenceNode>();
                            if (wallBuildArea.fenceObj == false)
                            {
                                Renderer r = wallBuildArea.GetComponent<Renderer>();
                                r.material.color = Color.blue;
                                FenceClass fenceEnumComponent = fenceToPlace.GetComponent<FenceClass>(); //get the Enum
                                Vector3 fencePos = hit.collider.gameObject.transform.position;
                                GameObject fenceActualPlaced = Instantiate(fenceToPlace, fencePos, Quaternion.identity) as GameObject;
                                fenceActualPlaced.transform.rotation = hit.collider.gameObject.transform.rotation;
                                Level_Object fencePlacedProperties = fenceActualPlaced.GetComponent<Level_Object>();
                                fencePlacedProperties.gridPosX = Mathf.RoundToInt(fencePos.x);
                                fencePlacedProperties.gridPosZ = Mathf.RoundToInt(fencePos.z);
                                wallBuildArea.fenceObj = true;
                                fenceActualPlaced.transform.parent = wallBuildArea.transform;
                                manager.inSceneFences.Add(fenceActualPlaced);
                                NumberTrackers.totalMoney -= fencePlacedProperties.price;
                                NumberTrackers.maintenance += fencePlacedProperties.maintenance;
                                switch (fenceEnumComponent.fenceType)
                                {
                                    case FenceClass.FenceTypes.CONCRETE:
                                        NumberTrackers.noConcrete++;
                                        break;
                                    case FenceClass.FenceTypes.CONCRETEW:
                                        NumberTrackers.noConcreteW++;
                                        break;
                                    case FenceClass.FenceTypes.GLASS:
                                        NumberTrackers.noGlass++;
                                        break;
                                    case FenceClass.FenceTypes.WIRE:
                                        NumberTrackers.noWire++;
                                        break;
                                    case FenceClass.FenceTypes.WOODEN:
                                        NumberTrackers.noWooden++;
                                        break;
                                    case FenceClass.FenceTypes.WOODENW:
                                        NumberTrackers.noWoodenW++;
                                        break;
                                }

                                starTracking.FencesCheck(animFences);
                                starTracking.CostCheck(animCost); 
                            }
                        }
                    }
                }
            }
            else
            {
                if (fenceClone != null)
                {
                    Destroy(fenceClone);
                }
            }
        }

        public void PassFenceToPlace(string fenceId)
        {
            if (fenceClone != null)
            {
                Destroy(fenceClone);
            }
            CloseAll();
            fenceHas = true;
            fenceClone = null;
            fenceToPlace = ResourcesManager.GetInstance().GetFenceBase(fenceId).fencePrefab;
        }

        #endregion

        //Going to change Animal Placement so it just adds in a penguin without you placing it, this will make it easier for deleting.
        #region Place animal
        public void PlaceAnimal()
        {
            if (animalHas)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (!ui.mouseOverUIElement && curNode != null)
                {
                    worldPosition = curNode.vis.transform.position;
                }
                if (animalClone == null)
                {
                    animalClone = Instantiate(animalToPlace, worldPosition, Quaternion.identity) as GameObject;
                    animalProperties = animalClone.GetComponent<Level_Object>();
                }
                else
                {
                    animalClone.transform.position = worldPosition;
                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                    {

                        if (hit.collider.tag == "EnclosureMarker")
                        {
                            GameObject animalActualPlaced = Instantiate(animalToPlace, worldPosition, animalClone.transform.rotation) as GameObject;
                            Level_Object animalPlacedProperties = animalActualPlaced.GetComponent<Level_Object>();
                            AnimalClass animalClass = animalActualPlaced.GetComponent<AnimalClass>();

                            manager.inSceneAnimals.Add(animalActualPlaced);

                            NumberTrackers.totalMoney -= animalPlacedProperties.price;

                            NumberTrackers.noAnimals++;
                            /*if(animalClass.Carnivore == true)
                            {
                                NumberTrackers.noAnimalsC++;
                            }
                            else if (animalClass.Herbivore == true)
                            {
                                NumberTrackers.noAnimalsH++;
                            }
                            else if (animalClass.Omnivore == true)
                            {
                                NumberTrackers.noAnimalsO++;
                            }*/

                        }
                        else
                        {
                            Debug.Log("Not ANIMAL Valid");
                        }
                    }


                    if (Input.GetMouseButtonDown(1))
                    {
                        animalProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (animalClone != null)
                {
                    Destroy(animalClone);
                }
            }
        }

        public void PassAnimalToPlace(string animalId)
        {
            if (animalClone != null)
            {
                Destroy(animalClone);
            }

            CloseAll();
            animalHas = true;
            animalClone = null;
            animalToPlace = ResourcesManager.GetInstance().GetAnimalBase(animalId).animalPrefab;
        }
        void DeleteAnimal()
        {
            if (animalDelete)
            {
                UpdateMousePosition();

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                {
                    if (hit.collider.tag == "penguin")
                    {
                        AnimalClass animalClass = hit.collider.gameObject.GetComponent<AnimalClass>();
                        Level_Object animalPlacedProperties = hit.collider.gameObject.GetComponent<Level_Object>();
                        manager.inSceneAnimals.Remove(GameObject.Find(hit.collider.gameObject.name));
                        Destroy(GameObject.Find(hit.collider.gameObject.name));

                        NumberTrackers.totalMoney += animalPlacedProperties.price;

                        NumberTrackers.noAnimals--;
                        NumberTrackers.noAnimalsC--;
                        starTracking.CareCheck(animCare);
                        starTracking.EnrichmentCheck(animEnrichment);
                        starTracking.TerrainCheck(animTerrain);
                        starTracking.CostCheck(animCost);
                        /* if (animalClass.Carnivore == true)
                         {
                             NumberTrackers.noAnimalsC--;
                         }
                         else if (animalClass.Herbivore == true)
                         {
                             NumberTrackers.noAnimalsH--;
                         }
                         else if (animalClass.Omnivore == true)
                         {
                             NumberTrackers.noAnimalsO--;
                         }*/
                    }
                }
            }
        }
        public void DeleteAnimals()
        {
            CloseAll();
            animalDelete = true;
        }
        #endregion

        #region Tile Painting

        void PaintTile()
        {
            if (hasMaterial)
            {
                UpdateMousePosition();
                if (hit.collider.tag == "EnclosureMarker")
                {
                    Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                    int matId = ResourcesManager.GetInstance().GetMaterialID(matToPlace);

                    if (previousNode == null)
                    {
                        previousNode = curNode;
                        prevMaterial = previousNode.tileRenderer.material;
                        prevRotation = previousNode.vis.transform.rotation;
                    }
                    else
                    {
                        if (previousNode != curNode)
                        {
                            if (paintTile)
                            {
                                matId = ResourcesManager.GetInstance().GetMaterialID(matToPlace);

                                if (curNode.vis.GetComponent<NodeObject>().textureid != matId)
                                {
                                    deleteTerrainChooser();
                                    
                                }

                                if (curNode.terrainObj == null)
                                {
                                    curNode.vis.GetComponent<NodeObject>().textureid = matId;
                                    placeTerrainChooser();
                                  
                                }

                                paintTile = false;
                            }
                            else
                            {
                                previousNode.tileRenderer.material = prevMaterial;
                                previousNode.vis.transform.rotation = prevRotation;
                            }


                            previousNode = curNode;
                            prevMaterial = curNode.tileRenderer.material;
                            prevRotation = curNode.vis.transform.rotation;
                        }
                    }
                    curNode.tileRenderer.material = matToPlace;
                    curNode.vis.transform.localRotation = targetRot;



                    if (Input.GetMouseButton(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                    {
                        if (hit.collider.tag == "EnclosureMarker")
                        {
                            paintTile = true;
                        }
                    }

                }
            }
        }

        void deleteTerrainChooser()
        {
            
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
           
            if (curNode.terrainObj != null)
            {
                Terrain_Object terrainPlacedProperties = curNode.terrainObj.GetComponent<Terrain_Object>();
                manager.inSceneObject.Remove(curNode.terrainObj.gameObject);
                Destroy(curNode.terrainObj.gameObject);
                curNode.terrainObj = null;
                switch (terrainPlacedProperties.matId)
                {
                    case 0:
                        NumberTrackers.noGrass--;
                        break;
                    case 1:
                        NumberTrackers.noStone--;
                        break;
                    case 2:
                        NumberTrackers.noSand--;
                        break;
                    case 3:
                        NumberTrackers.noWater--;
                        break;
                }
                starTracking.TerrainCheck(animTerrain);
                starTracking.CostCheck(animCost);
            }
        }
        void placeTerrainChooser()
        {
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
            worldPosition = curNode.vis.transform.position;
            GameObject terrainActualPlaced = Instantiate(terrainToPlace, worldPosition, Quaternion.identity) as GameObject;
            Terrain_Object terrainPlacedProperties = terrainActualPlaced.GetComponent<Terrain_Object>();

            terrainPlacedProperties.gridPosX = curNode.nodePosX;
            terrainPlacedProperties.gridPosZ = curNode.nodePosZ;
            curNode.terrainObj = terrainPlacedProperties;
            manager.inSceneObject.Add(terrainActualPlaced);
            NumberTrackers.totalMoney -= terrainPlacedProperties.price;

            switch(terrainPlacedProperties.matId)
            {
                case 0:
                    NumberTrackers.noGrass++;
                    break;
                case 1:
                    NumberTrackers.noStone++;
                    break;
                case 2:
                    NumberTrackers.noSand++;
                    break;
                case 3:
                    NumberTrackers.noWater++;
                    break;
            }
            starTracking.TerrainCheck(animTerrain);
            starTracking.CostCheck(animCost);
        }

        public void PassMaterialToPaint(int matId)
        {
            CloseAll();
            matToPlace = ResourcesManager.GetInstance().GetMaterial(matId);
            switch (matId)
                {
                case 0:
                    terrainToPlace = terrainObject;
                    break;
                case 1:
                    terrainToPlace = terrainObject1;
                    break;
                case 2:
                    terrainToPlace = terrainObject2;
                    break;
                case 3:
                    terrainToPlace = terrainObject3;
                    break;

            }

            hasMaterial = true;
        }

        /*public void PaintAll()
        {
            for (int x = 0; x < gridBase.sizeX; x++)
            {
                for (int z = 0; z < gridBase.sizeZ; z++)
                {
                    gridBase.grid[x, z].tileRenderer.material = matToPlace;
                    int matId = ResourcesManager.GetInstance().GetMaterialID(matToPlace);
                    gridBase.grid[x, z].vis.GetComponent<NodeObject>().textureid = matId;
                }
            }
            previousNode = null;
        }*/

        #endregion


       /* #region Place Foliage
        void PlaceFoliage()
        {
            if (foliageHas)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
                if (hit.collider.tag == "EnclosureMarker")
                {
                    worldPosition = curNode.vis.transform.position;
                }
                if (foliageClone == null)
                {
                    foliageClone = Instantiate(foliageToPlace, worldPosition, Quaternion.identity) as GameObject;
                    foliageProperties = foliageClone.GetComponent<Level_Object>();
                    foliageFoliageClass = foliageClone.GetComponent<FoliageClass>();
                }
                else
                {
                    foliageClone.transform.position = worldPosition;
                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                    {
                        if (hit.collider.tag == "EnclosureMarker")
                        {
                            if (curNode.placedObj != null)
                            {
                                Debug.Log("Cannot Place Object in the way");
                            }
                            else
                            {
                                if (curNode.vis.GetComponent<NodeObject>().textureid == 3)
                                {
                                    Debug.Log("Cannot Place Object in water");
                                }
                                else
                                {
                                    placingFoliage();
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Not Valid");
                        }
                    }


                    if (Input.GetMouseButtonDown(1))
                    {
                        foliageProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (foliageClone != null)
                {
                    Destroy(foliageClone);
                }
            }
        }

        void deletingFoliage()
        {
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            FoliageClass foliageEnumComponents = curNode.placedObj.gameObject.GetComponent<FoliageClass>(); //get the Enum before Deleting
            Level_Object foliagePlacedProperties = curNode.placedObj.gameObject.GetComponent<Level_Object>();

            NumberTrackers.totalMoney += foliagePlacedProperties.price;
            NumberTrackers.maintenance -= foliagePlacedProperties.maintenance;

            manager.inSceneEnrichment.Remove(curNode.placedObj.gameObject);
            Destroy(curNode.placedObj.gameObject);
            curNode.placedObj = null;

            //Subtract from No. trackers based on enum
            switch (foliageEnumComponents.foliageType)
            {
                case FoliageClass.FoliageTypes.BUSH:
                    NumberTrackers.noBush--;
                    break;
                case FoliageClass.FoliageTypes.ROCK:
                    NumberTrackers.noRock--;
                    break;
                case FoliageClass.FoliageTypes.OTHER:
                    NumberTrackers.noOther--;
                    break;
            }
        }
        void placingFoliage()
        {

            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            GameObject foliageActualPlaced = Instantiate(foliageToPlace, worldPosition, foliageClone.transform.rotation) as GameObject;
            Level_Object foliagePlacedProperties = foliageActualPlaced.GetComponent<Level_Object>();
            FoliageClass foliageEnumComponent = foliageActualPlaced.GetComponent<FoliageClass>(); //get the Enum

            foliagePlacedProperties.gridPosX = curNode.nodePosX;
            foliagePlacedProperties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = foliagePlacedProperties;
            manager.inSceneEnrichment.Add(foliageActualPlaced);

            NumberTrackers.totalMoney -= foliagePlacedProperties.price;
            NumberTrackers.maintenance += foliagePlacedProperties.maintenance;

            switch (foliageEnumComponent.foliageType)
            {
                case FoliageClass.FoliageTypes.BUSH:
                    NumberTrackers.noBush++;
                    break;
                case FoliageClass.FoliageTypes.ROCK:
                    NumberTrackers.noRock++;
                    break;
                case FoliageClass.FoliageTypes.OTHER:
                    NumberTrackers.noOther++;
                    break;
            }
        }


        public void PassFoliageToPlace(string foliageId)
        {
            if (foliageClone != null)
            {
                Destroy(foliageClone);
            }

            CloseAll();
            foliageHas = true;
            foliageClone = null;
            foliageToPlace = ResourcesManager.GetInstance().GetFoliageBase(foliageId).foliagePrefab;
        }
        void DeleteFoliage()
        {
            if (foliageDelete)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        deletingFoliage();
                    }
                }
            }
        }
        public void DeleteFoliages()
        {
            CloseAll();
            foliageDelete = true;
        }
        #endregion

        #region Place Enrichment
        void PlaceEnrichment()
        {

            if (enrichmentHas)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                if (enrichmentClone == null)
                {
                    enrichmentClone = Instantiate(enrichmentToPlace, worldPosition, Quaternion.identity) as GameObject;
                    enrichmentProperties = enrichmentClone.GetComponent<Level_Object>();
                    enrichmentToyClass = enrichmentClone.GetComponent<ToyClass>();
                }
                else
                {
                    enrichmentClone.transform.position = worldPosition;
                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                    {

                        if (hit.collider.tag == "EnclosureMarker")
                        {
                            if (curNode.placedObj != null)
                            {
                                Debug.Log("Cannot Place Object in the way");
                                //Replace?
                                /*if (curNode.placedObj.isEnrichmentObject == true)
                                {
                                    deletingEnrichment();
                                    placingEnrichment();
                                }
                                else
                                {
                                    Debug.Log("Not an enrichment object so cant replace");
                                }

                            }
                            else
                            {
                                //Water current texture id = 3, be sure to change when rearranging textures
                                if (curNode.vis.GetComponent<NodeObject>().textureid == 3 && enrichmentToyClass.isWaterObject == true)
                                {
                                    placingEnrichment();
                                }
                                else if (curNode.vis.GetComponent<NodeObject>().textureid == 3 && enrichmentToyClass.isWaterObject == false)
                                {
                                    Debug.Log("Cant place in Water");
                                }
                                else if (curNode.vis.GetComponent<NodeObject>().textureid != 3 && enrichmentToyClass.isWaterObject == false)
                                {
                                    placingEnrichment();
                                }
                                else if (curNode.vis.GetComponent<NodeObject>().textureid != 3 && enrichmentToyClass.isWaterObject == true)
                                {
                                    Debug.Log("Can only be placed in Water");
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("cant place outside of enclosure");
                        }
                    }


                    if (Input.GetMouseButtonDown(1))
                    {
                        enrichmentProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (enrichmentClone != null)
                {
                    Destroy(enrichmentClone);
                }
            }
        }

        void deletingEnrichment()
        {
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            ToyClass toyEnumComponents = curNode.placedObj.gameObject.GetComponent<ToyClass>(); //get the Enum before Deleting
            Level_Object enrichmentPlacedProperties = curNode.placedObj.gameObject.GetComponent<Level_Object>();

            NumberTrackers.totalMoney -= enrichmentPlacedProperties.price;
            NumberTrackers.maintenance += enrichmentPlacedProperties.maintenance;

            manager.inSceneEnrichment.Remove(curNode.placedObj.gameObject);
            Destroy(curNode.placedObj.gameObject);
            curNode.placedObj = null;

            //Subtract from No. trackers based on enum
            switch (toyEnumComponents.toyType)
            {
                case ToyClass.ToyTypes.TOY:
                    NumberTrackers.noToys--;
                    break;
                case ToyClass.ToyTypes.WATERTOY:
                    NumberTrackers.noWaterToys--;
                    break;
            }
            Happiness.maxAnimalsHappy -= toyEnumComponents.animalsKeptHappy;
        }
        void placingEnrichment()
        {

            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            GameObject enrichmentActualPlaced = Instantiate(enrichmentToPlace, worldPosition, enrichmentClone.transform.rotation) as GameObject;
            Level_Object enrichmentPlacedProperties = enrichmentActualPlaced.GetComponent<Level_Object>();
            ToyClass toyEnumComponent = enrichmentActualPlaced.GetComponent<ToyClass>(); //get the Enum

            enrichmentPlacedProperties.gridPosX = curNode.nodePosX;
            enrichmentPlacedProperties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = enrichmentPlacedProperties;
            manager.inSceneEnrichment.Add(enrichmentActualPlaced);

            NumberTrackers.totalMoney -= enrichmentPlacedProperties.price;
            NumberTrackers.maintenance += enrichmentPlacedProperties.maintenance;

            switch (toyEnumComponent.toyType)
            {
                case ToyClass.ToyTypes.TOY:
                    NumberTrackers.noToys++;
                    break;
                case ToyClass.ToyTypes.WATERTOY:
                    NumberTrackers.noWaterToys++;
                    break;
            
            }
            //Happiness.maxAnimalsHappy += toyEnumComponent.animalsKeptHappy;
        }

        public void PassEnrichmentToPlace(string enrichmentId)
        {
            if (enrichmentClone != null)
            {
                Destroy(enrichmentClone);
            }

            CloseAll();
            enrichmentHas = true;
            enrichmentClone = null;
            enrichmentToPlace = ResourcesManager.GetInstance().GetEnrichmentBase(enrichmentId).enrichmentPrefab;
        }
        void DeleteEnrichment()
        {
            if (enrichmentDelete)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        deletingEnrichment();
                    }
                }
            }
        }
        public void DeleteEnrichments()
        {
            CloseAll();
            enrichmentDelete = true;
        }
        #endregion

        #region Place Care
        void PlaceCare()
        {
            if (careHas)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
                Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

                Node nodeEE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (2 * gridBase.offset), 0, mousePosition.z));
                Node nodeWW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (2 * gridBase.offset), 0, mousePosition.z));
                Node nodeNN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (2 * gridBase.offset)));
                Node nodeSS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (2 * gridBase.offset)));

                worldPosition = curNode.vis.transform.position;

                if (careClone == null)
                {
                    careClone = Instantiate(careToPlace, worldPosition, Quaternion.identity) as GameObject;
                    careProperties = careClone.GetComponent<Level_Object>();
                }
                else
                {
                    careClone.transform.position = worldPosition;
                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)
                    {
                        if (hit.collider.tag == "EnclosureMarker")
                        {
                            CareClass careEnumComponent = careToPlace.GetComponent<CareClass>(); //get the Enum

                            switch (careEnumComponent.careType)
                            {
                                case CareClass.CareTypes.AID:

                                    if (NumberTrackers.noAid < 1)
                                    {
                                        GameObject careActualPlacedAid = Instantiate(careToPlace, worldPosition, careClone.transform.rotation) as GameObject;
                                        Level_Object carePlacedPropertiesAid = careActualPlacedAid.GetComponent<Level_Object>();
                                        carePlacedPropertiesAid.gridPosX = curNode.nodePosX;
                                        carePlacedPropertiesAid.gridPosZ = curNode.nodePosZ;

                                        curNode.placedObj = carePlacedPropertiesAid;
                                        nodeE.placedObj = carePlacedPropertiesAid;
                                        nodeW.placedObj = carePlacedPropertiesAid;
                                        nodeN.placedObj = carePlacedPropertiesAid;
                                        nodeS.placedObj = carePlacedPropertiesAid;
                                        nodeEE.placedObj = carePlacedPropertiesAid;
                                        nodeWW.placedObj = carePlacedPropertiesAid;
                                        nodeNN.placedObj = carePlacedPropertiesAid;
                                        nodeSS.placedObj = carePlacedPropertiesAid;

                                        manager.inSceneCare.Add(careActualPlacedAid);
                                        NumberTrackers.totalMoney -= carePlacedPropertiesAid.price;
                                        NumberTrackers.maintenance += carePlacedPropertiesAid.maintenance;

                                        NumberTrackers.noAid++;
                                    }

                                    break;
                                case CareClass.CareTypes.FOOD:
                                    GameObject careActualPlaced = Instantiate(careToPlace, worldPosition, careClone.transform.rotation) as GameObject;
                                    Level_Object carePlacedProperties = careActualPlaced.GetComponent<Level_Object>();
                                    carePlacedProperties.gridPosX = curNode.nodePosX;
                                    carePlacedProperties.gridPosZ = curNode.nodePosZ;
                                    curNode.placedObj = carePlacedProperties;
                                    manager.inSceneCare.Add(careActualPlaced);
                                    NumberTrackers.totalMoney -= carePlacedProperties.price;
                                    NumberTrackers.maintenance += carePlacedProperties.maintenance;

                                    switch (careEnumComponent.foodType)
                                    {
                                        case CareClass.FoodType.CARNIVOUROUS:
                                            NumberTrackers.noCarnivorous++;
                                            break;
                                        case CareClass.FoodType.HERBIVOROUS:
                                            NumberTrackers.noHerbivorous++;
                                            break;
                                    }

                                    break;
                                case CareClass.CareTypes.SHELTER:


                                    if (NumberTrackers.noShelter < 1)
                                    {
                                        GameObject careActualPlacedShelter = Instantiate(careToPlace, worldPosition, careClone.transform.rotation) as GameObject;
                                        Level_Object carePlacedPropertiesShelter = careActualPlacedShelter.GetComponent<Level_Object>();

                                        carePlacedPropertiesShelter.gridPosX = curNode.nodePosX;
                                        carePlacedPropertiesShelter.gridPosZ = curNode.nodePosZ;
                                        curNode.placedObj = carePlacedPropertiesShelter;


                                        nodeE.placedObj = carePlacedPropertiesShelter;
                                        nodeW.placedObj = carePlacedPropertiesShelter;
                                        nodeN.placedObj = carePlacedPropertiesShelter;
                                        nodeS.placedObj = carePlacedPropertiesShelter;
                                        nodeEE.placedObj = carePlacedPropertiesShelter;
                                        nodeWW.placedObj = carePlacedPropertiesShelter;
                                        nodeNN.placedObj = carePlacedPropertiesShelter;
                                        nodeSS.placedObj = carePlacedPropertiesShelter;

                                        manager.inSceneCare.Add(careActualPlacedShelter);
                                        NumberTrackers.totalMoney -= carePlacedPropertiesShelter.price;
                                        NumberTrackers.maintenance += carePlacedPropertiesShelter.maintenance;
                                        NumberTrackers.noShelter++;
                                        NumberTrackers.noAnimalsSheltered += careEnumComponent.noAnimalsPerShelter;
                                    }
                                    break;
                            }

                        }

                    }
                    //rotate
                    if (Input.GetMouseButtonDown(1))
                    {
                        careProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (careClone != null)
                {
                    Destroy(careClone);
                }
            }
        }

        public void PassCareToPlace(string careId)
        {
            if (careClone != null)
            {
                Destroy(careClone);
            }

            CloseAll();
            careHas = true;
            careClone = null;
            careToPlace = ResourcesManager.GetInstance().GetCareBase(careId).carePrefab;
        }
        void DeleteCare()
        {
            if (careDelete)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);


                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ui.mouseOverUIElement)

                {
                    if (curNode.placedObj != null)
                    {
                        CareClass careEnumComponents = curNode.placedObj.gameObject.GetComponent<CareClass>(); //get the Enum before Deleting
                        Level_Object carePlacedProperties = curNode.placedObj.gameObject.GetComponent<Level_Object>();

                        if (manager.inSceneCare.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneCare.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                        }
                        curNode.placedObj = null;

                        //Subtract from No. trackers based on enum
                        switch (careEnumComponents.careType)
                        {
                            case CareClass.CareTypes.AID:
                                NumberTrackers.noAid--;
                                NumberTrackers.totalMoney += carePlacedProperties.price;
                                NumberTrackers.maintenance -= carePlacedProperties.maintenance;
                                break;
                            case CareClass.CareTypes.FOOD:
                                switch (careEnumComponents.foodType)
                                {
                                    case CareClass.FoodType.CARNIVOUROUS:
                                        NumberTrackers.noCarnivorous--;
                                        break;
                                    case CareClass.FoodType.HERBIVOROUS:
                                        NumberTrackers.noHerbivorous--;
                                        break;
                                }
                                NumberTrackers.totalMoney += carePlacedProperties.price;
                                NumberTrackers.maintenance -= carePlacedProperties.maintenance;
                                break;
                            case CareClass.CareTypes.SHELTER:
                                NumberTrackers.noShelter--;
                                NumberTrackers.totalMoney += carePlacedProperties.price;
                                NumberTrackers.maintenance -= carePlacedProperties.maintenance;
                                break;
                        }
                    }
                }
            }
        }
        public void DeleteCares()
        {
            CloseAll();
            careDelete = true;
        }
        #endregion*/
       

        /*
        #region Wall Object Placers
        void PlaceWallObject()
        {
            if (hasWallObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;




                if (cloneWallObj == null)
                {
                    cloneWallObj = Instantiate(wallObjToPlace, worldPosition, Quaternion.identity) as GameObject;
                    wallObjProperties = cloneWallObj.GetComponent<Level_Object>();
                }
                else
                {
                    cloneWallObj.transform.position = worldPosition;

                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                    {

                        if (hit.collider.tag == "Wall")
                        {
                            GameObject actualWallObjPlaced = Instantiate(wallObjToPlace, worldPosition, cloneWallObj.transform.rotation) as GameObject;
                            Level_Object placedWallObjProperties = actualWallObjPlaced.GetComponent<Level_Object>();

                            placedWallObjProperties.gridPosX = curNode.nodePosX;
                            placedWallObjProperties.gridPosZ = curNode.nodePosZ;
                            curNode.placedObj = placedWallObjProperties;
                            manager.inSceneGameObjects.Add(actualWallObjPlaced);

                            totalMoney -= placedWallObjProperties.price;
                        }
                        else
                        {
                            Debug.Log("Cannot");
                        }
                    }
                }


            }
            else
            {
                if (cloneWallObj != null)
                {
                    Destroy(cloneWallObj);
                }
            }
        }


        public void PassWallObjectToPlace(string wallObjId)
        {
            if (cloneWallObj != null)
            {
                Destroy(cloneWallObj);
            }

            CloseAll();
            hasWallObj = true;
            cloneWallObj = null;
            wallObjToPlace = ResourcesManager.GetInstance().GetWallObjBase(wallObjId).objPrefab;
        }
        void DeleteWallObjs()
        {
            if (deleteWallObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        if (manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            totalMoney += curNode.placedObj.price;
                        }
                        curNode.placedObj = null;
                    }
                }
            }
        }
        public void DeleteWallObj()
        {
            CloseAll();
            deleteWallObj = true;
        }
        #endregion
       
        */
        /*#region Place Enclosure
        void PlaceEnclosure()
        {
            if (hasEnclosure)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

                Node nodeEE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (2 * gridBase.offset), 0, mousePosition.z));
                Node nodeWW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (2 * gridBase.offset), 0, mousePosition.z));
                Node nodeNN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (2 * gridBase.offset)));
                Node nodeSS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (2 * gridBase.offset)));

                Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
                Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));


                bool north = true;
                bool east = true;
                bool west = true;
                bool south = true;

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {


                    if (curNode.placedObj != null)
                    {
                        manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                        Destroy(curNode.placedObj.gameObject);
                        curNode.placedObj = null;
                    }


                    GameObject actualObjPlaced = Instantiate(enclosureToPlace, worldPosition, Quaternion.identity) as GameObject;
                    Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();
                    placedObjProperties.gridPosX = curNode.nodePosX;
                    placedObjProperties.gridPosZ = curNode.nodePosZ;
                    curNode.placedObj = placedObjProperties;
                    manager.inSceneWalls.Add(actualObjPlaced);


                    BuildWallsAroundEnclosure();
                    /*
                     GameObject actualObjPlaced = Instantiate(enclosureToPlace, worldPosition, Quaternion.identity) as GameObject;
                    Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();
                    Level_Wall placedWallProperties = actualObjPlaced.GetComponent<Level_Wall>();

                    placedObjProperties.gridPosX = curNode.nodePosX;
                    placedObjProperties.gridPosZ = curNode.nodePosZ;
                    curNode.placedObj = placedObjProperties;
                    manager.inSceneWalls.Add(actualObjPlaced);
                    curNode.wall = placedWallProperties;


                    if (nodeN.placedObj != null)
                    {
                        if (nodeN.placedObj.isEnclosureObject == true)
                        {

                            nodeN.wall.UpdateCorners(north, east, west, false);
                            north = false;
                        }
                        if (nodeNN.placedObj != null)
                        {
                            if (nodeNN.placedObj.isEnclosureObject == true)
                            {

                                nodeN.wall.UpdateCorners(false, east, west, false);
                                north = false;
                            }
                        }
                    }
                    if (nodeE.placedObj != null)
                    {
                        if (nodeE.placedObj.isEnclosureObject == true)
                        {

                            
                            nodeE.wall.UpdateCorners(north, east, false, south);
                            east = false;

                        }
                        if (nodeEE.placedObj != null)
                        {
                            if (nodeEE.placedObj.isEnclosureObject == true)
                            {


                                nodeE.wall.UpdateCorners(north, false, false, south);
                                east = false;

                            }
                        }
                    }
                    if (nodeW.placedObj != null)
                    {
                        if (nodeW.placedObj.isEnclosureObject == true)
                        {

                            
                            nodeW.wall.UpdateCorners(north, false, west, south);
                            west = false;

                        }
                        if (nodeWW.placedObj != null)
                        {
                            if (nodeWW.placedObj.isEnclosureObject == true)
                            {
                                nodeW.wall.UpdateCorners(north, false, false, south);
                                west = false;
                            }
                        }
                    }
                    if (nodeS.placedObj != null)
                    { 
                        if (nodeS.placedObj.isEnclosureObject == true)
                        {
                            
                            nodeS.wall.UpdateCorners(false, east, west, south);
                            south = false;
                        }
                        if (nodeSS.placedObj != null)
                        {
                            if (nodeSS.placedObj.isEnclosureObject == true)
                            {
                                nodeS.wall.UpdateCorners(false, east, west, false);
                                south = false;
                            }
                        }
                    }
                 
                    if (nodeE.placedObj != null && nodeN.placedObj != null)
                    {
                        if (nodeE.placedObj.isEnclosureObject == true && nodeN.placedObj.isEnclosureObject == true)
                        {
                            north = true;
                            east = true;
                            south = false;
                            west = true;

                            nodeN.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = true;
                            south = true;
                            west = false;

                            nodeE.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = false;
                            south = true;
                            west = true;
                        }
                    }
                    if (nodeE.placedObj != null && nodeS.placedObj != null)
                    {
                        if (nodeE.placedObj.isEnclosureObject == true && nodeS.placedObj.isEnclosureObject == true)
                        {
                            north = false;
                            east = true;
                            south = true;
                            west = true;

                            nodeS.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = true;
                            south = true;
                            west = false;

                            nodeE.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = false;
                            west = true;
                        }
                    }
                    if (nodeN.placedObj != null && nodeW.placedObj != null)
                    {
                        if (nodeN.placedObj.isEnclosureObject == true && nodeW.placedObj.isEnclosureObject == true)
                        {
                            north = true;
                            east = true;
                            south = false;
                            west = true;

                            nodeN.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = true;
                            west = true;

                            nodeW.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = true;
                            south = true;
                            west = false;

                        }
                    }
                    if (nodeS.placedObj != null && nodeW.placedObj != null)
                    {
                        if (nodeS.placedObj.isEnclosureObject == true && nodeW.placedObj.isEnclosureObject == true)
                        {
                            north = false;
                            east = true;
                            south = true;
                            west = true;

                            nodeS.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = true;
                            west = true;

                            nodeW.wall.UpdateCorners(north, east, west, south);


                        }
                    }
                    if (nodeS.placedObj != null && nodeW.placedObj != null && nodeE.placedObj != null && nodeN.placedObj != null)
                    {
                        if (nodeS.placedObj.isEnclosureObject == true && nodeW.placedObj.isEnclosureObject == true&& nodeE.placedObj.isEnclosureObject == true && nodeN.placedObj.isEnclosureObject == true)
                        {
                            north = true;
                            east = true;
                            south = false;
                            west = true;

                            nodeN.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = true;
                            south = true;
                            west = false;

                            nodeE.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = true;
                            west = true;

                            nodeW.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = true;
                            south = true;
                            west = true;

                            nodeS.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = false;
                            south = false;
                            west = false;
                        }
                    }

                    curNode.wall.UpdateCorners(north, east, west, south);*/
        /*

                }
                else
                {
                    if (cloneEnclosure != null)
                    {
                        Destroy(cloneEnclosure);
                    }
                }
            }
        }

        void UpdateWalls(Node node, bool a, bool b, bool c,bool d)
        {
             UpdateMousePosition();
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
            Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

            if (curNode.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeN.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeN.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeE.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeE.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeW.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeW.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeS.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeS.wall.UpdateCorners(a, b, c, d);
            }
        }

        public void PassEnclosureObjectToPlace()
        {
            if (cloneEnclosure != null)
            {
                Destroy(cloneEnclosure);
            }

            CloseAll();
            hasEnclosure = true;
            cloneEnclosure = null;
            enclosureToPlace = enclosureObject;
        }
        void DeleteEnclosures()
        {
            if (deleteEnclosure)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        if (manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            totalMoney += curNode.placedObj.price;
                        }
                        curNode.placedObj = null;
                    }
                }
            }
        }
        public void DeleteEnclosure()
        {
            CloseAll();
            deleteEnclosure = true;
        }

        public void DoneBuildingEnclosure()
        {
            CloseAll();
            BuildWallsAroundEnclosure();
        }
        
        void BuildWallsAroundEnclosureV2()
        {
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
            worldPosition = curNode.vis.transform.position;

            Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

            Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
            Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));


            actualWallPlaced = Instantiate(wallPrefab, new Vector3(worldPosition.x + (0.5f*gridBase.offset),0,worldPosition.z), Quaternion.Euler(0, 90, 0)) as GameObject;
            Level_Object placedNodeProperties = actualWallPlaced.GetComponent<Level_Object>();
            placedNodeProperties.gridPosX = curNode.nodePosX;
            placedNodeProperties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNodeProperties;
            manager.inSceneGameObjects.Add(actualWallPlaced);

            GameObject actualWallPlaced2 = Instantiate(wallPrefab, new Vector3(worldPosition.x - (0.5f * gridBase.offset), 0, worldPosition.z), Quaternion.Euler(0, 90, 0)) as GameObject;
            Level_Object placedNode1Properties = actualWallPlaced2.GetComponent<Level_Object>();
            placedNode1Properties.gridPosX = curNode.nodePosX;
            placedNode1Properties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNode1Properties;
            manager.inSceneGameObjects.Add(actualWallPlaced2);

            GameObject actualWallPlaced3 = Instantiate(wallPrefab, new Vector3(worldPosition.x, 0, worldPosition.z - (0.5f * gridBase.offset)), Quaternion.Euler(0, 0, 0)) as GameObject;
            Level_Object placedNode2Properties = actualWallPlaced3.GetComponent<Level_Object>();
            placedNode2Properties.gridPosX = curNode.nodePosX;
            placedNode2Properties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNode2Properties;
            manager.inSceneGameObjects.Add(actualWallPlaced3);

            GameObject actualWallPlaced4 = Instantiate(wallPrefab, new Vector3(worldPosition.x, 0, worldPosition.z + (0.5f * gridBase.offset)), Quaternion.Euler(0, 0, 0)) as GameObject;
            Level_Object placedNode3Properties = actualWallPlaced4.GetComponent<Level_Object>();
            placedNode3Properties.gridPosX = curNode.nodePosX;
            placedNode3Properties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNode3Properties;
            manager.inSceneGameObjects.Add(actualWallPlaced4);

           

        }

        void BuildWallsAroundEnclosure()
        {



            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

            Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
            Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));

            Node nodeEE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (2 * gridBase.offset), 0, mousePosition.z));
            Node nodeWW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (2 * gridBase.offset), 0, mousePosition.z));
            Node nodeNN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (2 * gridBase.offset)));
            Node nodeSS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (2 * gridBase.offset)));

            worldPosition = curNode.vis.transform.position;

            nodePosN = nodeN.vis.transform.position;
            nodePosS = nodeS.vis.transform.position;
            nodePosE = nodeE.vis.transform.position;
            nodePosW = nodeW.vis.transform.position;

            nodePosNE = nodeNE.vis.transform.position;
            nodePosSE = nodeSE.vis.transform.position;
            nodePosNW = nodeNW.vis.transform.position;
            nodePosSW = nodeSW.vis.transform.position;

                if (nodeN.placedObj == null)
            {


                actualWallPlaced = Instantiate(wallPrefab, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;

                Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeNProperties.gridPosX = nodeN.nodePosX;
                placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                nodeN.placedObj = placedNodeNProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);
            }
            if (nodeN.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                Destroy(nodeN.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;

                Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeNProperties.gridPosX = nodeN.nodePosX;
                placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                nodeN.placedObj = placedNodeNProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }

            if (nodeNE.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosNE, Quaternion.Euler(0, 180, 0)) as GameObject;
                Level_Object placedNodeNEProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                nodeNE.placedObj = placedNodeNEProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);

            }

            if (nodeNW.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;
                Level_Object placedNodeNWProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                nodeNW.placedObj = placedNodeNWProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);

            }

            if (nodeS.placedObj == null)
            {
                actualWallPlaced = Instantiate(wallPrefab, nodePosS, Quaternion.Euler(0, 0, 0)) as GameObject;
                Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeSProperties.gridPosX = nodeS.nodePosX;
                placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                nodeS.placedObj = placedNodeSProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

            }
            if (nodeS.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                Destroy(nodeS.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosS, Quaternion.Euler(0, 0, 0)) as GameObject;

                Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeSProperties.gridPosX = nodeS.nodePosX;
                placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                nodeS.placedObj = placedNodeSProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }
            if (nodeSE.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosSE, Quaternion.Euler(0, 270, 0)) as GameObject;
                Level_Object placedNodeSEProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                nodeSE.placedObj = placedNodeSEProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);

            }


            if (nodeSW.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;
                Level_Object placedNodeSWProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                nodeSW.placedObj = placedNodeSWProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);
            }

            if (nodeE.placedObj == null)
            {
                actualWallPlaced = Instantiate(wallPrefab, nodePosE, Quaternion.Euler(0, 90, 0)) as GameObject;
                Level_Object placedNodeEProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeEProperties.gridPosX = nodeE.nodePosX;
                placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                nodeE.placedObj = placedNodeEProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

            }
            if (nodeE.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                Destroy(nodeE.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosE, Quaternion.Euler(0, 90, 0)) as GameObject;

                Level_Object placedNodeEProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeEProperties.gridPosX = nodeE.nodePosX;
                placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                nodeE.placedObj = placedNodeEProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }
            if (nodeW.placedObj == null)
            {
                actualWallPlaced = Instantiate(wallPrefab, nodePosW, Quaternion.Euler(0, 90, 0)) as GameObject;
                Level_Object placedNodeWProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeWProperties.gridPosX = nodeW.nodePosX;
                placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                nodeW.placedObj = placedNodeWProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

            }
            if (nodeW.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                Destroy(nodeW.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosW, Quaternion.Euler(0, 90, 0)) as GameObject;

                Level_Object placedNodeWProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeWProperties.gridPosX = nodeW.nodePosX;
                placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                nodeW.placedObj = placedNodeWProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }



            if (nodeSW.placedObj.isEnclosureObject == true)
            {
                if(nodeS.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                    Destroy(nodeS.placedObj.gameObject);
                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosS, Quaternion.Euler(0, 90, 0)) as GameObject;
                    Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeSProperties.gridPosX = nodeS.nodePosX;
                    placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                    nodeS.placedObj = placedNodeSProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeW.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                    Destroy(nodeW.placedObj.gameObject);
                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosW, Quaternion.Euler(0, 270, 0)) as GameObject;
                    Level_Object placedNodeWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                    placedNodeWProperties.gridPosX = nodeW.nodePosX;
                    placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                    nodeW.placedObj = placedNodeWProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                Debug.Log("not null");
            }
            if (nodeSE.placedObj.isEnclosureObject == true)
            {
                if (nodeS.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                    Destroy(nodeS.placedObj.gameObject);


                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosS, Quaternion.Euler(0, 180, 0)) as GameObject;
                    Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeSProperties.gridPosX = nodeS.nodePosX;
                    placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                    nodeS.placedObj = placedNodeSProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeE.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                    Destroy(nodeE.placedObj.gameObject);
                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosE, Quaternion.Euler(0, 0, 0)) as GameObject;
                    Level_Object placedNodeEProperties = actualWallPlaced2.GetComponent<Level_Object>();
                    placedNodeEProperties.gridPosX = nodeE.nodePosX;
                    placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                    nodeE.placedObj = placedNodeEProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                Debug.Log("not null");
            }

            if (nodeNW.placedObj.isEnclosureObject == true)
            {
                if (nodeN.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                    Destroy(nodeN.placedObj.gameObject);

                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;

                    Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeNProperties.gridPosX = nodeN.nodePosX;
                    placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                    nodeN.placedObj = placedNodeNProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeW.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                    Destroy(nodeW.placedObj.gameObject);

                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosW, Quaternion.Euler(0, 180, 0)) as GameObject;
                    Level_Object placedNodeWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                    placedNodeWProperties.gridPosX = nodeW.nodePosX;
                    placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                    nodeW.placedObj = placedNodeWProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                

                Debug.Log("not null");

            }

            if (nodeNE.placedObj.isEnclosureObject == true)
            {
                if (nodeN.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                    Destroy(nodeN.placedObj.gameObject);

                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosN, Quaternion.Euler(0, 270, 0)) as GameObject;

                    Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeNProperties.gridPosX = nodeN.nodePosX;
                    placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                    nodeN.placedObj = placedNodeNProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeE.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                    Destroy(nodeE.placedObj.gameObject);
                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosE, Quaternion.Euler(0, 90, 0)) as GameObject;
                    Level_Object placedNodeEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                    placedNodeEProperties.gridPosX = nodeE.nodePosX;
                    placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                    nodeE.placedObj = placedNodeEProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                Debug.Log("not null");
            }
            if (nodeEE.placedObj != null)
            {
                if (nodeE.placedObj.isEnclosureObject == false && nodeEE.placedObj.isEnclosureObject == true)
                {
                    if (nodeNE.placedObj.isWallObject == true && nodeSE.placedObj.isWallObject == true)
                    {
                        actualWallPlaced = Instantiate(wallCornerPrefab, nodePosNE, Quaternion.Euler(0, 270, 0)) as GameObject;

                        Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                        placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                        nodeNE.placedObj = placedNodeNEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosSE, Quaternion.Euler(0, 180, 0)) as GameObject;

                        Level_Object placedNodeSEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                        placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                        nodeSE.placedObj = placedNodeSEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                    else if (nodeNE.placedObj.isCornerObject == true && nodeSE.placedObj.isCornerObject == true)
                    {
                        actualWallPlaced = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                        placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                        nodeNE.placedObj = placedNodeNEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeSEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                        placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                        nodeSE.placedObj = placedNodeSEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                }
            }
                if (nodeNN.placedObj != null)
                {
                    if (nodeN.placedObj.isEnclosureObject == false && nodeNN.placedObj.isEnclosureObject == true)
                    {
                        if (nodeNE.placedObj.isWallObject == true && nodeNW.placedObj.isWallObject == true)
                        {
                            actualWallPlaced = Instantiate(wallCornerPrefab, nodePosNE, Quaternion.Euler(0, 180, 0)) as GameObject;

                            Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                            placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                            placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                            nodeNE.placedObj = placedNodeNEProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced);

                            GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;

                            Level_Object placedNodeNWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                            placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                            placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                            nodeNW.placedObj = placedNodeNWProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced2);
                        }
                        else if (nodeNE.placedObj.isCornerObject == true && nodeNW.placedObj.isCornerObject == true)
                        {
                            actualWallPlaced = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 90, 0)) as GameObject;

                            Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                            placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                            placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                            nodeNE.placedObj = placedNodeNEProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced);

                            GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;

                            Level_Object placedNodeNWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                            placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                            placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                            nodeNW.placedObj = placedNodeNWProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced2);
                        }
                    }
                }
            if (nodeWW.placedObj != null)
            {
                if (nodeW.placedObj.isEnclosureObject == false && nodeWW.placedObj.isEnclosureObject == true)
                {
                    if (nodeNW.placedObj.isWallObject == true && nodeSW.placedObj.isWallObject == true)
                    {
                        actualWallPlaced = Instantiate(wallCornerPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;

                        Level_Object placedNodeNWProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                        placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                        nodeNW.placedObj = placedNodeNWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                        placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                        nodeSW.placedObj = placedNodeSWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                    else if (nodeNW.placedObj.isCornerObject == true && nodeSW.placedObj.isCornerObject == true)
                    {
                        actualWallPlaced = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeNWProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                        placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                        nodeNW.placedObj = placedNodeNWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                        placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                        nodeSW.placedObj = placedNodeSWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                }
            }
                        if (nodeSS.placedObj != null)
                        {
                            if (nodeS.placedObj.isEnclosureObject == false && nodeSS.placedObj.isEnclosureObject == true)
                            {
                                if (nodeSE.placedObj.isWallObject == true && nodeSW.placedObj.isWallObject == true)
                                {
                                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosSE, Quaternion.Euler(0, 0, 0)) as GameObject;

                                    Level_Object placedNodeSEProperties = actualWallPlaced.GetComponent<Level_Object>();

                                    placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                                    placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                                    nodeSE.placedObj = placedNodeSEProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced);

                                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosSW, Quaternion.Euler(0, 270, 0)) as GameObject;

                                    Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                    placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                                    placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                                    nodeSW.placedObj = placedNodeSWProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                                }
                                else if (nodeSE.placedObj.isCornerObject == true && nodeSW.placedObj.isCornerObject == true)
                                {
                                    actualWallPlaced = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 90, 0)) as GameObject;

                                    Level_Object placedNodeSEProperties = actualWallPlaced.GetComponent<Level_Object>();

                                    placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                                    placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                                    nodeSE.placedObj = placedNodeSEProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced);

                                    GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 90, 0)) as GameObject;

                                    Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                    placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                                    placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                                    nodeSW.placedObj = placedNodeSWProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                                }
                            }
                        }
                    */
        /*
                            if (nodeNN.placedObj !=null)
                            {
                                if (nodeNN.placedObj.isEnclosureObject == true)
                                {
                                    manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                                    Destroy(nodeN.placedObj.gameObject);
                                    GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;
                                    Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                                    placedNodeNProperties.gridPosX = nodeN.nodePosX;
                                    placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                                    nodeN.placedObj = placedNodeNProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced);

                                   /* manager.inSceneGameObjects.Remove(nodeNE.placedObj.gameObject);
                                    Destroy(nodeNE.placedObj.gameObject);
                                    GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 90, 0)) as GameObject;
                                    Level_Object placedNodeNEProperties = actualWallPlaced1.GetComponent<Level_Object>();

                                    placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                                    placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                                    nodeNE.placedObj = placedNodeNEProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced1);

                                    manager.inSceneGameObjects.Remove(nodeNW.placedObj.gameObject);
                                    Destroy(nodeNW.placedObj.gameObject);
                                    GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;
                                    Level_Object placedNodeNWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                    placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                                    placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                                    nodeNW.placedObj = placedNodeNWProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced2);*/
        /*
                                            }
                                        }
                                        if (nodeEE.placedObj != null)
                                        {
                                            if (nodeEE.placedObj.isEnclosureObject == true)
                                            {
                                                manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                                                Destroy(nodeE.placedObj.gameObject);
                                                GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosE, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeEProperties = actualWallPlaced.GetComponent<Level_Object>();

                                                placedNodeEProperties.gridPosX = nodeE.nodePosX;
                                                placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                                                nodeE.placedObj = placedNodeEProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced);

                                               /* manager.inSceneGameObjects.Remove(nodeNE.placedObj.gameObject);
                                                Destroy(nodeNE.placedObj.gameObject);
                                                GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeNEProperties = actualWallPlaced1.GetComponent<Level_Object>();

                                                placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                                                placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                                                nodeNE.placedObj = placedNodeNEProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced1);

                                                manager.inSceneGameObjects.Remove(nodeSE.placedObj.gameObject);
                                                Destroy(nodeSE.placedObj.gameObject);
                                                GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeSEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                                placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                                                placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                                                nodeSE.placedObj = placedNodeSEProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced2);*/
        /*
                                            }
                                        }
                                        if (nodeWW.placedObj != null)
                                        {
                                            if (nodeWW.placedObj.isEnclosureObject == true)
                                            {
                                                manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                                                Destroy(nodeW.placedObj.gameObject);
                                                GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosW, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeWProperties = actualWallPlaced.GetComponent<Level_Object>();

                                                placedNodeWProperties.gridPosX = nodeW.nodePosX;
                                                placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                                                nodeW.placedObj = placedNodeWProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced);


                                                /*manager.inSceneGameObjects.Remove(nodeNW.placedObj.gameObject);
                                                Destroy(nodeNW.placedObj.gameObject);
                                                GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeNWProperties = actualWallPlaced1.GetComponent<Level_Object>();

                                                placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                                                placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                                                nodeNW.placedObj = placedNodeNWProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced1);

                                                manager.inSceneGameObjects.Remove(nodeSW.placedObj.gameObject);
                                                Destroy(nodeSW.placedObj.gameObject);
                                                GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                                placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                                                placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                                                nodeSW.placedObj = placedNodeSWProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced2);*/
        /*             }
                 }
                 if (nodeSS.placedObj != null)
                 {
                     if (nodeSS.placedObj.isEnclosureObject == true)
                     {
                         manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                         Destroy(nodeS.placedObj.gameObject);
                         GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosS, Quaternion.Euler(0, 0, 0)) as GameObject;
                         Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                         placedNodeSProperties.gridPosX = nodeS.nodePosX;
                         placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                         nodeS.placedObj = placedNodeSProperties;
                         manager.inSceneGameObjects.Add(actualWallPlaced);

                       /*  manager.inSceneGameObjects.Remove(nodeSE.placedObj.gameObject);
                         Destroy(nodeSE.placedObj.gameObject);
                         GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 90, 0)) as GameObject;
                         Level_Object placedNodeSEProperties = actualWallPlaced1.GetComponent<Level_Object>();

                         placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                         placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                         nodeSE.placedObj = placedNodeSEProperties;
                         manager.inSceneGameObjects.Add(actualWallPlaced1);

                         manager.inSceneGameObjects.Remove(nodeSW.placedObj.gameObject);
                         Destroy(nodeSW.placedObj.gameObject);
                         GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 90, 0)) as GameObject;
                         Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                         placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                         placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                         nodeSW.placedObj = placedNodeSWProperties;
                         manager.inSceneGameObjects.Add(actualWallPlaced2);*/
        /*     }
         }


    }
#endregion*/

    }
}

//Place Objects Code for Copy Paste amendments
/*#region Place Objects
void PlaceObject()
{
    if (hasObj)
    {
        UpdateMousePosition();

        Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

        worldPosition = curNode.vis.transform.position;

        if (cloneObj == null)
        {
            cloneObj = Instantiate(objToPlace, worldPosition, Quaternion.identity) as GameObject;
            objProperties = cloneObj.GetComponent<Level_Object>();
        }
        else
        {
            cloneObj.transform.position = worldPosition;
            if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
            {
                if (curNode.placedObj != null)
                {
                    manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                    Destroy(curNode.placedObj.gameObject);
                    curNode.placedObj = null;
                }

                GameObject actualObjPlaced = Instantiate(objToPlace, worldPosition, cloneObj.transform.rotation) as GameObject;
                Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                placedObjProperties.gridPosX = curNode.nodePosX;
                placedObjProperties.gridPosZ = curNode.nodePosZ;
                curNode.placedObj = placedObjProperties;
                manager.inSceneGameObjects.Add(actualObjPlaced);
                totalMoney -= placedObjProperties.price;
            }

            if (Input.GetMouseButtonDown(1))
            {
                objProperties.ChangeRotation();
            }
        }
    }
    else
    {
        if (cloneObj != null)
        {
            Destroy(cloneObj);
        }
    }
}

public void PassGameObjectToPlace(string objId)
{
    if (cloneObj != null)
    {
        Destroy(cloneObj);
    }

    CloseAll();
    hasObj = true;
    cloneObj = null;
    objToPlace = ResourcesManager.GetInstance().GetObjBase(objId).objPrefab;
}
void DeleteObjs()
{
    if (deleteObj)
    {
        UpdateMousePosition();

        Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

        if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
        {
            if (curNode.placedObj != null)
            {
                if (manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                {
                    manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                    Destroy(curNode.placedObj.gameObject);
                    totalMoney += curNode.placedObj.price;
                }
                curNode.placedObj = null;
            }
        }
    }
}
public void DeleteObj()
{
    CloseAll();
    deleteObj = true;
}
#endregion

 #region Stack Objects

        public void PassStackedObjectToPlace(string objId)
        {
            if (stackCloneObj != null)
            {
                Destroy(stackCloneObj);
            }

            CloseAll();
            placeStackObj = true;
            stackCloneObj = null;
            stackObjToPlace = ResourcesManager.GetInstance().GetStackObjBase(objId).objPrefab;
        }

        void PlaceStackedObj()
        {
            if (placeStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                if (stackCloneObj == null)
                {
                    stackCloneObj = Instantiate(stackObjToPlace, worldPosition, Quaternion.identity) as GameObject;
                    stackObjProperties = stackCloneObj.GetComponent<Level_Object>();
                }
                else
                {
                    stackCloneObj.transform.position = worldPosition;

                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                    {
                        GameObject actualObjPlaced = Instantiate(stackObjToPlace, worldPosition, stackCloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosZ = curNode.nodePosZ;
                        curNode.stackedObjs.Add(placedObjProperties);
                        manager.inSceneStackObjects.Add(actualObjPlaced);
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        stackObjProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (stackCloneObj != null)
                {
                    Destroy(stackCloneObj);
                }
            }
        }

        void DeleteStackedObjs()
        {
            if (deleteStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.stackedObjs.Count > 0)
                    {
                        for (int i = 0; i < curNode.stackedObjs.Count; i++)
                        {
                            if (manager.inSceneStackObjects.Contains(curNode.stackedObjs[i].gameObject))
                            {
                                manager.inSceneStackObjects.Remove(curNode.stackedObjs[i].gameObject);
                                Destroy(curNode.stackedObjs[i].gameObject);
                            }
                        }
                        curNode.stackedObjs.Clear();
                    }
                }
            }
        }
        public void DeleteStackedObj()
        {
            CloseAll();
            deleteStackObj = true;
        }
        #endregion */


/*#region Wall Creator

        public void OpenWallCreation()
        {
            CloseAll();
            createWall = true;
        }

        void CreateWall()
        {
            if (createWall)
            {
                UpdateMousePosition();
                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
                worldPosition = curNode.vis.transform.position;

                if (startNode_Wall == null)
                {
                    if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                    {
                        startNode_Wall = curNode;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        endNodeWall = curNode;
                    }
                }

                if (startNode_Wall != null && endNodeWall != null)
                {
                    int difX = endNodeWall.nodePosX - startNode_Wall.nodePosX;
                    int difZ = endNodeWall.nodePosZ - startNode_Wall.nodePosZ;

                    CreateWallInNode(startNode_Wall.nodePosX, startNode_Wall.nodePosZ, Level_WallObj.WallDirection.ab);

                    Node finalXNode = null;
                    Node finalZNode = null;

                    //if (difX == 0 && difZ == 0)
                    //{
                    //    CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.all);
                    //    UpdateWallCorners(endNodeWall, true, true, true);
                    //}

                    if (difX != 0)
                    {
                        bool xHigher = (difX > 0);

                        for (int i = 1; i < Mathf.Abs(difX) + 1; i++)
                        {
                            int offset = xHigher ? i : -i;
                            int posX = startNode_Wall.nodePosX + offset;
                            int posZ = startNode_Wall.nodePosZ;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            finalXNode = gridBase.grid[posX, posZ];
                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.ab;
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        UpdateWallCorners(xHigher ? endNodeWall : startNode_Wall, true, false, false);
                        UpdateWallCorners(xHigher ? startNode_Wall : endNodeWall, false, true, false);

                    }
                    if (difZ != 0)
                    {
                        bool zHigher = (difZ > 0);

                        for (int i = 1; i < Mathf.Abs(difZ) + 1; i++)
                        {
                            int offset = zHigher ? i : -i;
                            int posX = startNode_Wall.nodePosX;
                            int posZ = startNode_Wall.nodePosZ + offset;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.bc;

                            finalZNode = gridBase.grid[posX, posZ];
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        UpdateWallNode(startNode_Wall, Level_WallObj.WallDirection.bc);

                        UpdateWallCorners(zHigher ? startNode_Wall : finalZNode, false, true, false);
                        UpdateWallCorners(zHigher ? finalZNode : startNode_Wall, false, false, true);
                    }
                    if (difZ != 0 && difX != 0)
                    {
                        bool xHigher = (difX > 0);
                        bool zHigher = (difZ > 0);

                        for (int i = 1; i < Mathf.Abs(difX) + 1; i++)
                        {
                            int offset = xHigher ? i : -i;
                            int posX = startNode_Wall.nodePosX + offset;
                            int posZ = endNodeWall.nodePosZ;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.ab;
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        for (int i = 1; i < Mathf.Abs(difZ) + 1; i++)
                        {
                            int offset = zHigher ? i : -i;
                            int posX = endNodeWall.nodePosX;
                            int posZ = startNode_Wall.nodePosZ + offset;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.bc;
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        if (startNode_Wall.nodePosZ > endNodeWall.nodePosZ)
                        {
                            #region Up to down
                            manager.inSceneWalls.Remove(finalXNode.wallObj.gameObject);
                            Destroy(finalXNode.wallObj.gameObject);
                            finalXNode.wallObj = null;

                            UpdateWallNode(finalZNode, Level_WallObj.WallDirection.all);
                            UpdateWallNode(endNodeWall, Level_WallObj.WallDirection.bc);

                            if (startNode_Wall.nodePosX > endNodeWall.nodePosX)
                            {
                                #region End node is SW of Start
                                //the furthest node on the x
                                CreateWallOrUpdateNode(finalXNode, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(finalXNode, false, true, false);
                                //the end furthest to south
                                CreateWallOrUpdateNode(finalZNode, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(finalZNode, false, true, false);
                                //first node clicked
                                //destroy node and get on next to it
                                Node nextToStartNode = DestroyCurrentNodeAndGetPrevious(startNode_Wall, true);
                                UpdateWallCorners(nextToStartNode, true, false, false);
                                //the end node clicked
                                CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(endNodeWall, false, true, false);
                                #endregion
                            }
                            else
                            {
                                #region End node is SE of start
                                //the furthest node on the x
                                Node beforeFinalX = DestroyCurrentNodeAndGetPrevious(finalXNode, true);
                                UpdateWallCorners(beforeFinalX, true, false, false);
                                //the end node furthest south
                                CreateWallOrUpdateNode(finalZNode, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(finalZNode, false, true, false);
                                //first node clicked
                                //destroy and get on next to it
                                CreateWallOrUpdateNode(startNode_Wall, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(startNode_Wall, false, true, false);
                                //the end node
                                CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(endNodeWall, false, true, false);
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region Down to up
                            if (startNode_Wall.nodePosX > endNodeWall.nodePosX)
                            {
                                #region End node is NW of Start
                                //the furthest node on the north east
                                Node northWestNode = DestroyCurrentNodeAndGetPrevious(finalZNode, true);
                                UpdateWallCorners(northWestNode, true, false, false);
                                //the end furthest to south west
                                CreateWallOrUpdateNode(finalXNode, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(finalXNode, false, true, false);
                                //first node clicked
                                //destroy node and get on next to it
                                CreateWallOrUpdateNode(startNode_Wall, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(startNode_Wall, false, true, false);
                                //the end node clicked
                                CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(endNodeWall, false, true, false);
                                #endregion
                            }
                            else
                            {
                                #region End node is NE of start
                                //the furthest node on the north west
                                CreateWallOrUpdateNode(finalZNode, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(finalZNode, false, true, false);
                                //the end node furthest south east
                                CreateWallOrUpdateNode(finalXNode, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(finalXNode, false, true, false);
                                //first node clicked
                                CreateWallOrUpdateNode(startNode_Wall, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(startNode_Wall, false, true, false);
                                //the end node
                                Node nextToEndNode = DestroyCurrentNodeAndGetPrevious(endNodeWall, true);
                                UpdateWallCorners(nextToEndNode, true, false, false);
                                #endregion
                            }
                            #endregion
                        }
                    }
                    startNode_Wall = null;
                    endNodeWall = null;

                }
            }
        }

        void CreateWallOrUpdateNode(Node getNode, Level_WallObj.WallDirection direction)
        {
            if (getNode.wallObj == null)
            {
                CreateWallInNode(getNode.nodePosX, getNode.nodePosZ, direction);
            }
            else
            {
                UpdateWallNode(getNode, direction);
            }
        }

        Node DestroyCurrentNodeAndGetPrevious(Node curNode, bool positive)
        {
            int i = (positive) ? 1 : -1;

            Node beforeCurNode = gridBase.grid[curNode.nodePosX - i, curNode.nodePosZ];

            if (curNode.wallObj != null)
            {
                if (manager.inSceneWalls.Contains(curNode.wallObj.gameObject))
                {
                    manager.inSceneWalls.Remove(curNode.wallObj.gameObject);
                    Destroy(curNode.wallObj.gameObject);
                    curNode.wallObj = null;
                }
            }
            return beforeCurNode;
        }

        void CreateWallInNode(int posX, int posZ, Level_WallObj.WallDirection direction)
        {
            Node getNode = gridBase.grid[posX, posZ];
            Vector3 wallPosition = getNode.vis.transform.position;
            if (getNode.wallObj == null)
            {
                GameObject actualObjPlaced = Instantiate(wallPrefab, wallPosition, Quaternion.identity) as GameObject;
                actualObjPlaced.transform.parent = manager.wallHolder.transform;

                Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();
                Level_WallObj placedWallProperties = actualObjPlaced.GetComponent<Level_WallObj>();

                placedObjProperties.gridPosX = posX;
                placedObjProperties.gridPosZ = posZ;
                manager.inSceneWalls.Add(actualObjPlaced);
                getNode.wallObj = placedWallProperties;

                UpdateWallNode(getNode, direction);
            }
            else
            {
                UpdateWallNode(getNode, direction);
            }
            UpdateWallCorners(getNode, false, false, false);
        }

        void UpdateWallNode(Node getNode, Level_WallObj.WallDirection direction)
        {
            // for loop not neccessary??
            //for (int i = 0; i<getNode.wallObj.wallsList.Count;i++)
            // {
            getNode.wallObj.UpdateWall(direction);
            // }
        }

        void UpdateWallCorners(Node getNode, bool a, bool b, bool c)
        {
            if (getNode.wallObj != null)
            {
                getNode.wallObj.UpdateCorners(a, b, c);
            }
        }

        public void DeleteWall()
        {
            CloseAll();
            deleteWall = true;
        }

        void DeleteWallsActual()
        {
            if (deleteWall)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.wallObj != null)
                    {
                        if (manager.inSceneWalls.Contains(curNode.wallObj.gameObject))
                        {
                            manager.inSceneWalls.Remove(curNode.wallObj.gameObject);
                            Destroy(curNode.wallObj.gameObject);
                        }
                        curNode.wallObj = null;
                    }
                }
            }
        }

        #endregion*/

