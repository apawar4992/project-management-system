import http from "../http-common";

class SubCategoryService{
     GetSubCategories(categoryId) {
       return http.get(`/subcategory/${categoryId}`);
    }

    GetAllSubCategories(){
        return http.get(`/subcategory`);
    }
}

export default new SubCategoryService();