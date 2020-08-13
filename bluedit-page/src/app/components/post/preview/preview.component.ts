import { Component, OnInit } from '@angular/core';
import { PostPreview } from '../post.preview.model'

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.css']
})
export class PreviewComponent implements OnInit {

  post : PostPreview

  constructor() { }

  ngOnInit(): void {
  }

}
