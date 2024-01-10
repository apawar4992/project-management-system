import http from "../http-common";

class ProductDataService{
    GetProductsBySubCategoryId(subCategoryId) {
       return http.get(`/products/${subCategoryId}`);
    }

    GetProducts() {
        return http.get(`/products`);
     }

     AddProduct(product) {
         return http.post(`/products`, product);
     }

     EditProduct(product, productId){
        return http.put(`/products/${productId}`, product);
     }
}

export default new ProductDataService();
