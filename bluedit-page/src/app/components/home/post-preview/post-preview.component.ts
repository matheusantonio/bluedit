import { Component, OnInit, Input, INJECTOR } from '@angular/core';
import { PostPreview } from './post-preview-model';

@Component({
  selector: 'app-post-preview',
  templateUrl: './post-preview.component.html',
  styleUrls: ['./post-preview.component.css']
})
export class PostPreviewComponent implements OnInit {

  //@Input() post : PostPreview

  @Input() title : string
  @Input() content : string

  constructor() { }

  ngOnInit(): void {
  }

}
