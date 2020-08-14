import { Component, OnInit, Input } from '@angular/core';
import { PostPreview } from '../../../models/post.preview.model'

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.css']
})
export class PreviewComponent implements OnInit {

  @Input() post : PostPreview

  constructor() { }

  ngOnInit(): void {
  }

}
