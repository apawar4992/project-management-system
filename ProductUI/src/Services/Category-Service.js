import http from '../http-common';

class CategoryService{
    GetCategories() {
      return http.get(`/category`)
    }
}

export default new CategoryService();