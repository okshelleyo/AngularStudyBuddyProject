import { FavoritesApiService } from './../services/favorites-api.service';
import { Favorites } from './../models/Favorites';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-favorites-list',
  templateUrl: './favorites-list.component.html',
  styleUrls: ['./favorites-list.component.css']
})
export class FavoritesListComponent implements OnInit {

  favoriteList: Favorites[] = [];

  constructor(private favoriteService: FavoritesApiService, private route: ActivatedRoute) { }

  ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    let user: string = routeParams.get("user");
    this.getAllFavoritesByUser(user);
  }

  getAllFavoritesByUser(user: string) {
    this.favoriteService.getFavoritesByUser(user).subscribe(
      result=> {
        this.favoriteList = result;
        console.log(this.favoriteList)
      },
      error => console.log(error)
    ) }

}
