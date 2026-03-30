import { Component, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ProjectsService } from '../../services/projects';
import { AuthService } from '../../services/auth';
import { User } from '../../models/user';
import { Project } from '../../models/project';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatSortModule, Sort } from '@angular/material/sort';

@Component({
  selector: 'app-info',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatSortModule
  ],
  templateUrl: './info.html',
  styleUrl: './info.scss'
})
export class InfoComponent implements OnInit {
  user = signal<User | null>(null);
  allProjects = signal<Project[]>([]);
  filteredProjects = signal<Project[]>([]);
  filterText = signal('');
  displayedColumns = ['name', 'score', 'durationInDays', 'bugsCount', 'madeDeadline'];

  averageScore = computed(() => {
    const projects = this.filteredProjects();
    if (!projects.length) return 0;
    return Math.round(
      projects.reduce((sum, p) => sum + p.score, 0) / projects.length
    );
  });

  deadlinePercentage = computed(() => {
    const projects = this.filteredProjects();
    if (!projects.length) return 0;
    const met = projects.filter(p => p.madeDeadline).length;
    return Math.round((met / projects.length) * 100);
  });

  constructor(
    private projectsService: ProjectsService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.user.set(this.authService.getUser());

    this.projectsService.getProjects().subscribe({
      next: (data) => {
        this.allProjects.set(data);
        this.filteredProjects.set(data);
      },
      error: () => {
        this.router.navigate(['/login']);
      }
    });
  }

  applyFilter(): void {
    const text = this.filterText().toLowerCase();
    this.filteredProjects.set(
      this.allProjects().filter(p => p.name.toLowerCase().includes(text))
    );
  }

  sortData(sort: Sort): void {
    if (!sort.active || sort.direction === '') {
      this.applyFilter();
      return;
    }

    this.filteredProjects.set([...this.filteredProjects()].sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'score': return compare(a.score, b.score, isAsc);
        case 'durationInDays': return compare(a.durationInDays, b.durationInDays, isAsc);
        case 'bugsCount': return compare(a.bugsCount, b.bugsCount, isAsc);
        case 'madeDeadline': return compare( a.madeDeadline ? 1 : 0, b.madeDeadline ? 1 : 0, isAsc);
        default: return 0;
      }
    }));
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}

function compare(a: string | number, b: string | number, isAsc: boolean): number {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}