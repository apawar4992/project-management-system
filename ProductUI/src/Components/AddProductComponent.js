import React, { useEffect, useContext } from "react";
import { useState, useMemo } from "react";
import Modal from 'react-modal';
import ProductService from '../Services/Product-Service';
import SubCategoryService from '../Services/SubCategory-Service';
import ProductContext from "../Contexts/ProductContext";

const AddProductComponent = ({ModalOpen, handelModelOpenClose, handelIsProductAddedOrUpdated, selectedSubCategoryId}) =>{

    const productContextValue = useContext(ProductContext);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [subCategories, setSubCategories] = useState([]);
    const [selectedSubId, setSelectedSubId] = useState(0);

    const [productData, setProductData] = useState({
        'productCode': '',
        'name':'',
        'quantity':'',
        'price':'',
        'description':'',
        'image':'',
        'subCategory':null
        });

        const fetchSubCategories = async () => {
            await SubCategoryService.GetAllSubCategories()
                                    .then(response => {
                                        setSubCategories(response.data);
                                    })
                                    .catch(e => {
                                        console.log('Error fetching all sub-categories:', e);
                                    });
        }

    // Use useMemo to memoize the fetchSubCategories function
    const memoizedFetchData = useMemo(() => fetchSubCategories, []);

    useEffect(() => {
        const loadEditData = () => {
            if(productContextValue !== null)
            {
                 setProductData(productContextValue);
                 setSelectedSubId(selectedSubCategoryId);
            }
        }

        setIsModalOpen(ModalOpen);
        memoizedFetchData();
        loadEditData();
    },[ModalOpen, productContextValue, selectedSubCategoryId, memoizedFetchData]);
    // Ensure useEffect runs when memoizedFetchData or other variable changes
    
    const handleAddOrEditProduct = () => {
        if(productContextValue === null)
        {
            addProduct();
        }
        else
        {
            editProduct();
        }
      setIsModalOpen(false);
      handelModelOpenClose(false);
    };

    const addProduct = () => {
            ProductService.AddProduct(productData)
            .then(response => {
                handelIsProductAddedOrUpdated(true);
            })
            .catch(e => {
                console.log('Error while adding product:', e);
            })
        }

    const editProduct = () => {
        productData.subCategory.name="dummy";
        ProductService.EditProduct(productData, productData.id)
        .then(response => {
            handelIsProductAddedOrUpdated(true);
        })
        .catch(e => {
            console.log('Error while editing product:', e);
        })
    }

    const handleCancelProduct = () => {
        setIsModalOpen(false);
        handelModelOpenClose(false);
    }

    const handleInputChange = (e) => {
        let { name, value } = e.target;
            if(e.target.name === 'subCategory')
            {
                setSelectedSubId(e.target.value);
                let newSubCategory = {};
                newSubCategory.Id = e.target.value;
                newSubCategory.name = 'subCategory';
                value = newSubCategory;
            }

        setProductData((prevData) => ({
          ...prevData,
          [name]: value,
        }));
      };

    return(
        <div>
            <Modal isOpen={isModalOpen} onRequestClose={() => setIsModalOpen(false)} contentLabel="Add Product" >
        <form>
            <h2 style={{textAlign: "center", color:"red"}}>Add Product</h2>
                <label>
                Product Code: &nbsp;
                <input
                type= "text"
                name= "productCode"
                value={productData.productCode}
                onChange={handleInputChange}
                />
            </label>
            <br />
            <br />
                <label>
                Name: &nbsp;
                <input
                type="text"
                name="name"
                value={productData.name}
                onChange={handleInputChange}
                />
            </label>
            <br />
            <br /> <label>
                Quantity: &nbsp;
                <input
                type="text"
                name="quantity"
                value={productData.quantity}
                onChange={handleInputChange} />
            </label>
            <br />
            <br /> <label>
                Price: &nbsp;
                <input
                type="text"
                name="price"
                value={productData.price}
                onChange={handleInputChange} />
            </label>
            <br />
            <br /> <label>
                Description: &nbsp;
                <input
                type="text"
                name="description"
                value={productData.description}
                onChange={handleInputChange} />
            </label>
            <br />
            <br /> <label>
                Image Link: &nbsp;
                <input
                type="text"
                name="image"
                value={productData.image}
                onChange={handleInputChange} />
            </label>
            <br />
            <br />
            <br />
            
            <div className='subcategory-section'>
                    <h4 style={{marginRight: 10}}>Subcategory</h4>
                    <select id="selectList" value={selectedSubId} name='subCategory' onChange= {handleInputChange}>
                    <option value={productData.subCategory}>Select Subcategory</option>
                    {
                        subCategories.map((subcategory) => (
                            <option key={subcategory.id} value={subcategory.id}>
                                {subcategory.name}
                            </option>
                        ))
                    }
                    </select>
            </div>

            <div className="submit-section">
            <button type="button" onClick={handleAddOrEditProduct}>Save</button>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <button onClick={handleCancelProduct}>Cancel</button>
            </div>
        </form>
      </Modal>
        </div>
    )
}

export default AddProductComponent;