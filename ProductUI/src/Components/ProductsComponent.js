import { useEffect, useState } from 'react';
import ProductService from '../Services/Product-Service';
import '../Css/table.css';
import '../Css/products-component.css'
import editIcon from '../Images/edit-246.png';
import CategoryService from "../Services/Category-Service";
import SubCategoryService from '../Services/SubCategory-Service';
import AddProductComponent from './AddProductComponent';
import ProductContext from '../Contexts/ProductContext';

const ProductsComponent = () => {
    const [products, setProducts] = useState([]);
    const [categories, setCategories] = useState([]);
    const [subCategories, setSubCategories] = useState([]);

    const [editProductData, setEditProductData] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isProductAddedOrUpdated, setIsProductAddedOrUpdated] = useState(false);
    const [selectedSubCategoryId, setSelectedSubCategoryId] = useState(0);

    const fetchProductData = async () => {
        await ProductService.GetProducts()
                                .then(response => {
                                    console.log(response.data);
                                    setProducts(response.data);
                                })
                                .catch(e => {
                                    console.log('Error fetching products list:', e);
                                });
    }

    const fetchCategories = async () => {
        await CategoryService.GetCategories()
                                .then(response => {
                                    console.log(response.data);
                                    setCategories(response.data);
                                })
                                .catch(e => {
                                    console.log('Error fetching categories list:', e);
                                });
    }

    useEffect(() => {
        return () => fetchCategories();
    }, [])
    
    useEffect(()=>{
        setIsProductAddedOrUpdated(false);
        // To prevent calling twice, it will destroy the current instance when it unmounts.
         return () =>  fetchProductData();
    },[isProductAddedOrUpdated])

    const editProduct = (identifier) => {
        let productToEdit = products.find(x=> x.id === identifier)
        setSelectedSubCategoryId(productToEdit.subCategory.id);
        setEditProductData(productToEdit);
        setIsModalOpen(true);
    }

    const addProduct = () => {
        setEditProductData(null);
        setIsModalOpen(true);
        setSelectedSubCategoryId(0);
    }

    const fetchSubCategories = async (id) =>{
        await SubCategoryService.GetSubCategories(id)
                                .then(response => {
                                    console.log(response.data);
                                    setSubCategories(response.data);
                                })
                                .catch(e => {
                                    console.log('Error fetching sub-categories list:', e);
                                });
    }

    const filterProducts = async (id) => {
            setSelectedSubCategoryId(id);
            await ProductService.GetProductsBySubCategoryId(id)
                                    .then(response => {
                                        console.log(response.data);
                                        setProducts(response.data);
                                    })
                                    .catch(e => {
                                          console.log('Error in filter products list:', e);
                                    });
        }

    const handelIsModelOpen = (ModelOpened) => {
        setIsModalOpen(ModelOpened);
    }   

    const handelIsProductAddedOrUpdated = (productAddedOrUpdated) => {
        setIsProductAddedOrUpdated(productAddedOrUpdated);
    }   

    const TableRow = ({ rowData }) => {
             // Dynamically set the class based on the condition
            let backgroundStyle;
            if(rowData.quantity >= 10 && rowData.quantity <= 100)
            {
                backgroundStyle = { backgroundColor: '#FF7F50'}
            }
            else if(rowData.quantity > 100)
            {
                backgroundStyle = { backgroundColor: '#AFE1AF'}
            }
            else if(rowData.quantity <10)
            {
                backgroundStyle = { backgroundColor: '#E34234'}
            }
   
        return (
        <tr style={backgroundStyle} key={rowData.id}>
            <td>
                <img alt={rowData.description} onClick={() => {editProduct(rowData.id)}} className='edit-icon' src={editIcon}></img>
            </td>
            <td>{rowData.productCode}</td>
            <td>{rowData.name}</td>
            <td>{rowData.quantity}</td>
            <td>&#8377; {rowData.price}</td>
            <td className='table-description'>{rowData.description}</td>
            <td>
                <img alt={rowData.description} className='table-image' src={rowData.image}/>
            </td>
        </tr>
        );
      };

    return(
        <div>
            <div className='filter-dropdown'>
                <div className='category-section'>
                <h4 style={{marginRight: 10}}>Category :</h4>
                <select onChange={(e) => fetchSubCategories(e.target.value) }>
                <option value="">Select Category</option>
                {
                    categories.map(category => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))
                }
                </select>
                </div>

                <div className='subcategory-section'>
                    <h4 style={{marginRight: 10}}>Subcategory Dropdown</h4>
                    <select onChange={(e) => filterProducts(e.target.value)}>
                    <option value="">Select Subcategory</option>
                    {
                        subCategories.map((subcategory) => (
                            <option key={subcategory.id} value={subcategory.id}>
                                {subcategory.name}
                            </option>
                        ))
                    }
                    </select>
                </div>

                <div className='product-section'>
                    <button onClick={() => addProduct()} className='product-add'>Add Product</button>
                </div>
            
            <ProductContext.Provider value={editProductData}>
                { isModalOpen && 
                    <AddProductComponent ModalOpen = {isModalOpen} handelModelOpenClose = {handelIsModelOpen} handelIsProductAddedOrUpdated = {handelIsProductAddedOrUpdated} selectedSubCategoryId = {selectedSubCategoryId}/> 
                }
            </ProductContext.Provider>
            </div>
            <div className="table-container">
            <table className="simple-table">
            <tbody>
            <tr>
                <th></th>
                <th>Product Code</th>
                <th>Name</th>
                <th>Quantity</th>
                <th>Price</th>
                <th>Description</th>
                <th>Image</th>
            </tr>
            {
                products.map(item => (
                    <TableRow key={item.productCode} rowData={item} />
                ))
            }
            </tbody>
            </table>
            </div>
        </div>
    )
}

export default ProductsComponent;